using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    /// <summary>
    /// Представляет связь между пользователем и ролью, которая назначена этому пользователю.
    /// </summary>
    public class RolesUsers
    {
        /// <summary>
        /// Идентификатор роли, назначенной пользователю, на основе перечисления <see cref="EnumTypeRoles"/>.
        /// </summary>
        public EnumTypeRoleDb RoleId { get; set; }

        /// <summary>
        /// Роль, назначенная пользователю.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому назначена роль.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь, которому назначена роль.
        /// </summary>
        public UserDb UserDb { get; set; }
    }
}
