using System;

namespace Project.Data
{
    /// <summary>
    /// Представляет дополнительную информацию о пользователе.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор записи о пользователе.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает идентификатор пользователя, связанного с этой информацией.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Получает или задает объект пользователя, связанного с этой информацией.
        /// </summary>
        public UserDb UserDb { get; set; }

        /// <summary>
        /// Получает или задает возраст пользователя.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Получает или задает дату создания записи о пользователе.
        /// </summary>
        public DateTime DateCreate { get; set; }
    }
}
