using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Data
{
    /// <summary>
    /// Модель пользователя для хранения данных в базе данных.
    /// </summary>
    public class UserDb
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Имя не может быть длиннее 100 символов.")]
        public string Name { get; set; }

        /// <summary>
        /// Возраст пользователя.
        /// </summary>
        [Range(0, 120, ErrorMessage = "Возраст должен быть в диапазоне от 0 до 120 лет.")]
        public int Age { get; set; }
    }
}
