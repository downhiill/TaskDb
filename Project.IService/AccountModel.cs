using System;

namespace Project.IService
{
    /// <summary>
    /// Представляет модель аккаунта, содержащую данные для авторизации и связи с пользователем.
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// Получает или задает логин аккаунта.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Получает или задает пароль аккаунта.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Получает или задает идентификатор пользователя, связанного с аккаунтом.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Получает или задает модель пользователя, связанного с аккаунтом.
        /// </summary>
        public UserModel UserModel { get; set; }
    }
}
