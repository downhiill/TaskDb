using System;
using System.Collections.Generic;

namespace Project.Data
{
    /// <summary>
    /// Представляет роль пользователя в системе.
    /// Содержит информацию о типе роли и её названии.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Уникальный идентификатор роли.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тип роли, определённый в <see cref="EnumTypeRoleDb"/>.
        /// </summary>
        public EnumTypeRoleDb Type { get; set; }

        /// <summary>
        /// Название роли.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список пользователей, связанные с данной ролью.
        /// </summary>
        public ICollection<UserDb> Users { get; set; } = new List<UserDb>();
    }
}
