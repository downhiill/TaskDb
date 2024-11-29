using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class ServiceProfession : IServiceProfession
    {
        private readonly ApplicationContext _context;

        /// <summary>
        /// Конструктор для инъекции зависимости ApplicationContext.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public ServiceProfession(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает список пользователей с указанной профессией и ролью.
        /// </summary>
        /// <param name="nameProfession">Название профессии.</param>
        /// <param name="role">Роль пользователя.</param>
        /// <returns>Список пользователей, соответствующих профессии и роли.</returns>
        public List<ShortUserProfessionRole> GetUserProfessionRole(string nameProfession, EnumTypeRoleModel role)
        {
            EnumTypeRoleDb dbRole = (EnumTypeRoleDb)(int)role; // Преобразование EnumTypeRoleModel в EnumTypeRoleDb

            return _context.Users
                .Where(u => u.Profession.Name == nameProfession && u.Roles.Any(r => r.Role.Id == dbRole))
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
                        Name = u.Roles.FirstOrDefault(r => r.Role.Id == dbRole).Role.Name
                    },
                    ProfessionName = u.Profession.Name
                })
                .ToList();
        }
    }
}
