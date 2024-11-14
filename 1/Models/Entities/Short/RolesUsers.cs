using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models.Entities
{
    public class RolesUsers
    {
        public EnumTypeRoles RoleId { get; set; }
        public Roles Role { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
