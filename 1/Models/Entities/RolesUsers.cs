using System;

namespace _1.Models.Entities
{
    /// <summary>
    /// Представляет связь между пользователем и ролью, которая назначена этому пользователю.
    /// </summary>
    public class RolesUsers
    {
        /// <summary>
        /// Идентификатор роли, назначенной пользователю, на основе перечисления <see cref="EnumTypeRoles"/>.
        /// </summary>
        public EnumTypeRoles RoleId { get; set; }

        /// <summary>
        /// Роль, назначенная пользователю.
        /// </summary>
        public Roles Role { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому назначена роль.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь, которому назначена роль.
        /// </summary>
        public User User { get; set; }
    }
}
