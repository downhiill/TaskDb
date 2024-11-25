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
            /// Получает или устанавливает уникальный идентификатор пользователя.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Получает или устанавливает имя пользователя.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Получает или устанавливает возраст пользователя.
            /// </summary>
            public int Age { get; set; }

            /// <summary>
            /// Получает или устанавливает дату создания пользователя.
            /// </summary>
            public DateTime DateCreate { get; set; }

            /// <summary>
            /// Получает или устанавливает заработную плату пользователя.
            /// </summary>
            public decimal Wages { get; set; }

            /// <summary>
            /// Получает или устанавливает состояние активности пользователя.
            /// </summary>
            public bool Active { get; set; }

            /// <summary>
            /// Получает или устанавливает дату рождения пользователя. Может быть null, если дата не указана.
            /// </summary>
            public DateTime? DateOfBirth { get; set; }
    }
}
