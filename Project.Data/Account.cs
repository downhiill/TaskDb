using System;

namespace Project.Data
{
    /// <summary>
    /// Представляет учетную запись пользователя.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Получает или задает логин учетной записи.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Получает или задает пароль учетной записи.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Получает или задает идентификатор пользователя, связанный с учетной записью.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Получает или задает пользователя, связанного с учетной записью.
        /// </summary>
        public UserDb UserDb { get; set; }
    }
}
