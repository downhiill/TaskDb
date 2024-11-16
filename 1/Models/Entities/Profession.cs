using System;
using System.Collections.Generic;

namespace _1.Models.Entities
{
    /// <summary>
    /// Представляет профессию, которая может быть связана с несколькими пользователями.
    /// </summary>
    public class Profession
    {
        /// <summary>
        /// Уникальный идентификатор профессии.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название профессии.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список пользователей, которые имеют эту профессию.
        /// </summary>
        public List<User> Users { get; set; }
    }
}
