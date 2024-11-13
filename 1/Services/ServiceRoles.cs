using _1.Models.Context;
using _1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Services
{
    public class ServiceRoles
    {
        private readonly ApplicationContext _db;

        public ServiceRoles()
        {
            _db = new ApplicationContext();
        }

        public void UserAddRole (int userId, EnumTypeRoles role)
        {
            // Создаем новый объект RolesUsers
            var roleUser = new RolesUsers
            {
                UserId = userId,
                RoleId = role
            };

            // Добавляем запись в таблицу RolesUsers
            _db.RolesUsers.Add(roleUser);
            _db.SaveChanges();
        }

        public void UserChangeRole (int userId, List<EnumTypeRoles> roles)
        {
            // Получаем все текущие роли пользователя
            var existingRoles = _db.RolesUsers.Where(ru => ru.UserId == userId).ToList();

            // Удаляем все текущие роли пользователя
            _db.RolesUsers.RemoveRange(existingRoles);

            // Создаем новые записи для каждой роли в списке `roles` и добавляем их
            var newRoles = roles.Select(role => new RolesUsers
            {
                UserId = userId,
                RoleId = role
            }).ToList();

            _db.RolesUsers.AddRange(newRoles);
            _db.SaveChanges();
        }

        public void UserRemoveRole(int userId, EnumTypeRoles role)
        {
            // Находим запись в таблице RolesUsers, где UserId и RoleId соответствуют переданным значениям
            var roleUser = _db.RolesUsers.FirstOrDefault(ru => ru.UserId == userId && ru.RoleId == role);

            // Если такая запись найдена, удаляем её
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

        public List<ShortUserRole> GetUsers(EnumTypeRoles role)
        {
                return _db.RolesUsers
                    .Where(ru => ru.RoleId == role) // Фильтруем по роли
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
                            Type = ru.Role.Id,  // Предполагается, что Role.Id это EnumTypeRoles
                            Name = ru.Role.Name
                        }
                    }).ToList();        
        }

        public List<Roles> GetRolesUser(int userId)
        {
            return _db.RolesUsers
            .Where(ru => ru.UserId == userId)  // Фильтруем по userId
            .Select(ru => ru.Role)  // Получаем роли, связанные с этим пользователем
            .ToList();  // Преобразуем в список
        }

        public List<ShortUserRoles> GetAllUsers()
        {
            return _db.RolesUsers
                .Where(ru => ru.Role != null)  // Фильтруем записи, чтобы выбрать только тех пользователей, у которых есть роли
                .GroupBy(ru => ru.UserId)  // Группируем по UserId, чтобы собрать все роли каждого пользователя
                .Select(group => new ShortUserRoles
                {
                    User = new ShortUser
                    {
                        Id = group.Key,  // Используем ключ группы (UserId)
                        Name = group.First().User.Name,  // Имя первого пользователя из группы
                        DateOfBirth = group.First().User.DateOfBirth  // Дата рождения первого пользователя из группы
                    },
                    Roles = group.Select(ru => new ShortRole
                    {
                        Type = ru.RoleId,  // Преобразуем RoleId в EnumTypeRoles
                        Name = ru.Role.Name  // Имя роли
                    }).ToList()  // Собираем список ролей для пользователя
                })
                .ToList();  // Преобразуем в список и возвращаем
        }

    }
}
