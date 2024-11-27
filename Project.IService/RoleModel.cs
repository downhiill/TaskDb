using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    public class RoleModel
    {
        public int Id { get; set; }


        public EnumTypeRoleModel Type { get; set; } // Тип роли, используя вложенный enum
        public string Name { get; set; } // Название роли

        // Связь с пользователями
        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
    }
}
