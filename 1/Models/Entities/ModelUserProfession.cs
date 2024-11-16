using System;

namespace _1.Models.Entities
{
    /// <summary>
    /// Модель пользователя с профессией, включающая имя пользователя и название профессии.
    /// </summary>
    public class ModelUserProfession
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Название профессии пользователя.
        /// </summary>
        public string ProfessionName { get; set; }
    }
}
