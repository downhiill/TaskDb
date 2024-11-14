using _1.Models.Context;
using _1.Models.Entities;
using _1.Models.Entities.Short;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Services
{
    public class ServiceProfession
    {
        private readonly ApplicationContext _db;

        public ServiceProfession()
        {
            _db = new ApplicationContext();
        }
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
