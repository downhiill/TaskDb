using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models.Entities
{
    public class Roles
    {
        public EnumTypeRoles Id { get; set; }
        public string Name { get; set; }
        public List<RolesUsers> Users { get; set; }
    }
}
