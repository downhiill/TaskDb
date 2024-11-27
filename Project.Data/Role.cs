using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public class Role
    {
        public int Id { get; set; }


        public EnumTypeRoleDb Type { get; set; } // Тип роли, используя вложенный enum
        public string Name { get; set; } // Название роли

        // Связь с пользователями
        public ICollection<UserDb> Users { get; set; } = new List<UserDb>();
    }
}
