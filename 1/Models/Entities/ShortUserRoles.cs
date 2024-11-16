using System;
using System.Collections.Generic;

namespace _1.Models.Entities
{
    /// <summary>
    /// Класс, представляющий пользователя и его роли.
    /// </summary>
    public class ShortUserRoles
    {
        /// <summary>
        /// Краткое описание пользователя.
        /// </summary>
        public ShortUser User { get; set; }

        /// <summary>
        /// Список ролей пользователя.
        /// </summary>
        public List<ShortRole> Roles { get; set; }
    }
}
