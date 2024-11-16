using System;
using System.Collections.Generic;

namespace _1.Models.Entities
{
    /// <summary>
    /// Представляет роль, которая может быть назначена пользователям.
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// Уникальный идентификатор роли, основанный на перечислении <see cref="EnumTypeRoles"/>.
        /// </summary>
        public EnumTypeRoles Id { get; set; }

        /// <summary>
        /// Название роли (например, "Админ", "Пользователь", "Гость").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список пользователей, которые имеют эту роль.
        /// </summary>
        public List<RolesUsers> Users { get; set; }
    }
}
