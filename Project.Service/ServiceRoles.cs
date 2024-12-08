using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class ServiceRoles : IServiceRoles
    {
        private readonly ApplicationContext _context;

        /// <summary>
        /// Конструктор для инъекции зависимости ApplicationContext.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public ServiceRoles(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет роль пользователю.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="role">Роль для добавления пользователю.</param>
        public void UserAddRole(int userId, EnumTypeRoleModel role)
        {
            var roleUser = new RolesUsers
            {
                UserId = userId,
                RoleId = (EnumTypeRoleDb)role
            };

            _context.RolesUsers.Add(roleUser);
            _context.SaveChanges();
        }

        /// <summary>
        /// Меняет роли пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="roles">Список новых ролей для пользователя.</param>
        public void UserChangeRole(int userId, List<EnumTypeRoleModel> roles)
        {
            // Удаление существующих ролей
            _context.RolesUsers
                .Where(ru => ru.UserId == userId)
                .ExecuteDelete();

            // Добавление новых ролей
            var newRoles = roles.Select(role => new RolesUsers
            {
                UserId = userId,
                RoleId = (EnumTypeRoleDb)role
            }).ToList();

            _context.RolesUsers.AddRange(newRoles);
            _context.SaveChanges();
        }


        /// <summary>
        /// Удаляет роль у пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="role">Роль, которую нужно удалить.</param>
        public void UserRemoveRole(int userId, EnumTypeRoleModel role)
        {
            var dbRole = (EnumTypeRoleDb)(int)role;

            // Удаление напрямую в базе данных
            var deletedCount = _context.RolesUsers
                .Where(ru => ru.UserId == userId && ru.RoleId == dbRole)
                .ExecuteDelete();

            if (deletedCount == 0)
            {
                // Вместо вывода на консоль выбрасываем исключение
                throw new InvalidOperationException($"Роль '{role}' не найдена для пользователя с ID {userId}.");
            }
        }

        /// <summary>
        /// Получает список пользователей, имеющих определенную роль.
        /// </summary>
        /// <param name="role">Роль, по которой будет выполнен поиск.</param>
        /// <returns>Список пользователей с указанной ролью.</returns>
        public List<ShortUserRole> GetUsers(EnumTypeRoleModel role)
        {
            var dbRole = (EnumTypeRoleDb)(int)role;

            return _context.RolesUsers
                .Where(ru => ru.RoleId == dbRole)
                .Select(ru => new ShortUserRole
                {
                    User = new ShortUser
                    {
                        Id = ru.UserDb.Id,
                        Name = ru.UserDb.Name,
                        DateOfBirth = ru.UserDb.DateOfBirth
                    },
                    Role = new ShortRole
                    {
                        Type = role,
                        Name = ru.Role.Name
                    }
                }).ToList();
        }

        /// <summary>
        /// Получает список ролей пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список ролей для указанного пользователя.</returns>
        public List<RoleModel> GetRolesUser(int userId)
        {
            return _context.RolesUsers
                .Where(ru => ru.UserId == userId)
                .Select(ru => new RoleModel
                {
                    Id = (EnumTypeRoleModel)(int)ru.Role.Id, // Преобразование EnumTypeRoleDb в EnumTypeRoleModel
                    Name = ru.Role.Name
                })
                .ToList();
        }

        /// <summary>
        /// Получает всех пользователей и их роли.
        /// </summary>
        /// <returns>Список всех пользователей с их ролями.</returns>
        public List<ShortUserRoles> GetAllUsers()
        {
            return _context.RolesUsers
                .Where(ru => ru.Role != null)
                .GroupBy(ru => ru.UserId)
                .Select(group => new ShortUserRoles
                {
                    User = new ShortUser
                    {
                        Id = group.Key,
                        Name = group.First().UserDb.Name,
                        DateOfBirth = group.First().UserDb.DateOfBirth
                    },
                    Roles = group.Select(ru => new ShortRole
                    {
                        Type = (EnumTypeRoleModel)(int)ru.Role.Id,
                        Name = ru.Role.Name
                    }).ToList()
                })
                .ToList();
        }
    }
}