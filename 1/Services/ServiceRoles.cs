using _1.Models.Context;
using _1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Services
{
    /// <summary>
    /// Сервис для работы с ролями пользователей.
    /// </summary>
    public class ServiceRoles
    {
        private readonly ApplicationContext _db;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ServiceRoles"/>.
        /// </summary>
        /// <param name="dbContext">Контекст базы данных для работы с данными.</param>
        public ServiceRoles(ApplicationContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Добавляет роль пользователю.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="role">Роль для добавления пользователю.</param>
        public void UserAddRole(int userId, EnumTypeRoles role)
        {
            var roleUser = new RolesUsers
            {
                UserId = userId,
                RoleId = role
            };

            _db.RolesUsers.Add(roleUser);
            _db.SaveChanges();
        }

        /// <summary>
        /// Меняет роли пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="roles">Список новых ролей для пользователя.</param>
        public void UserChangeRole(int userId, List<EnumTypeRoles> roles)
        {
            var existingRoles = _db.RolesUsers.Where(ru => ru.UserId == userId).ToList();
            _db.RolesUsers.RemoveRange(existingRoles);

            var newRoles = roles.Select(role => new RolesUsers
            {
                UserId = userId,
                RoleId = role
            }).ToList();

            _db.RolesUsers.AddRange(newRoles);
            _db.SaveChanges();
        }

        /// <summary>
        /// Удаляет роль у пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="role">Роль, которую нужно удалить.</param>
        public void UserRemoveRole(int userId, EnumTypeRoles role)
        {
            var roleUser = _db.RolesUsers.FirstOrDefault(ru => ru.UserId == userId && ru.RoleId == role);
            if (roleUser != null)
            {
                _db.RolesUsers.Remove(roleUser);
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Роль не найдена для этого пользователя.");
            }
        }

        /// <summary>
        /// Получает список пользователей, имеющих определенную роль.
        /// </summary>
        /// <param name="role">Роль, по которой будет выполнен поиск.</param>
        /// <returns>Список пользователей с указанной ролью.</returns>
        public List<ShortUserRole> GetUsers(EnumTypeRoles role)
        {
            return _db.RolesUsers
                .Where(ru => ru.RoleId == role)
                .Select(ru => new ShortUserRole
                {
                    User = new ShortUser
                    {
                        Id = ru.User.Id,
                        Name = ru.User.Name,
                        DateOfBirth = ru.User.DateOfBirth
                    },
                    Role = new ShortRole
                    {
                        Type = ru.Role.Id,
                        Name = ru.Role.Name
                    }
                }).ToList();
        }

        /// <summary>
        /// Получает список ролей пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список ролей для указанного пользователя.</returns>
        public List<Roles> GetRolesUser(int userId)
        {
            return _db.RolesUsers
                .Where(ru => ru.UserId == userId)
                .Select(ru => ru.Role)
                .ToList();
        }

        /// <summary>
        /// Получает всех пользователей и их роли.
        /// </summary>
        /// <returns>Список всех пользователей с их ролями.</returns>
        public List<ShortUserRoles> GetAllUsers()
        {
            return _db.RolesUsers
                .Where(ru => ru.Role != null)
                .GroupBy(ru => ru.UserId)
                .Select(group => new ShortUserRoles
                {
                    User = new ShortUser
                    {
                        Id = group.Key,
                        Name = group.First().User.Name,
                        DateOfBirth = group.First().User.DateOfBirth
                    },
                    Roles = group.Select(ru => new ShortRole
                    {
                        Type = ru.RoleId,
                        Name = ru.Role.Name
                    }).ToList()
                })
                .ToList();
        }
    }
}
