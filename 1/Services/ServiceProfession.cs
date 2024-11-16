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
    /// Сервис для работы с профессиями и ролями пользователей.
    /// </summary>
    public class ServiceProfession
    {
        private readonly ApplicationContext _db;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ServiceProfession"/>.
        /// </summary>
        /// <param name="dbContext">Контекст базы данных для работы с данными.</param>
        public ServiceProfession(ApplicationContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Получает список пользователей с указанной профессией и ролью.
        /// </summary>
        /// <param name="nameProfession">Название профессии.</param>
        /// <param name="role">Роль пользователя.</param>
        /// <returns>Список пользователей, соответствующих профессии и роли.</returns>
        public List<ShortUserProfessionRole> GetUserProfessionRole(string nameProfession, EnumTypeRoles role)
        {
            return _db.Users
                .Where(u => u.Profession.Name == nameProfession && u.Roles.Any(r => r.Role.Id == role))
                .Select(u => new ShortUserProfessionRole
                {
                    User = new ShortUser
                    {
                        Id = u.Id,
                        Name = u.Name,
                        DateOfBirth = u.DateOfBirth
                    },
                    Role = new ShortRole
                    {
                        Type = role,
                        Name = u.Roles.FirstOrDefault(r => r.Role.Id == role).Role.Name
                    },
                    ProfessionName = u.Profession.Name
                })
                .ToList();
        }
    }
}
