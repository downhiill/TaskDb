using System;

namespace _1.Models.Entities
{
    /// <summary>
    /// Класс, представляющий краткое описание пользователя.
    /// </summary>
    public class ShortUser
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения пользователя.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
    }
}
