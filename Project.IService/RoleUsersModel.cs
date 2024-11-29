using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    /// <summary>
    /// Представляет связь между пользователем и ролью, которая назначена этому пользователю.
    /// </summary>
    public class RolesUsersModel
    {
        /// <summary>
        /// Идентификатор роли, назначенной пользователю, на основе перечисления <see cref="EnumTypeRoles"/>.
        /// </summary>
        public EnumTypeRoleModel RoleId { get; set; }

        /// <summary>
        /// Роль, назначенная пользователю.
        /// </summary>
        public RoleModel RoleModel { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому назначена роль.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь, которому назначена роль.
        /// </summary>
        public UserModel UserModel { get; set; }
    }
}
