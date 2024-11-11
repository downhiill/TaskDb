using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models
{
    public class Role
    {
        public int Id { get; set; }

        // Вложенное перечисление ролей
        public enum EnumTypeRoles
        {
            User,
            Guest,
            Admin
        }

        public EnumTypeRoles Type { get; set; } // Тип роли, используя вложенный enum
        public string Name { get; set; } // Название роли

        // Связь с пользователями
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
