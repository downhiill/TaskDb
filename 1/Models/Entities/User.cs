using System;
using System.Collections.Generic;

namespace _1.Models.Entities
{
    /// <summary>
    /// Класс, представляющий пользователя с его данными.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Возраст пользователя.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Заработная плата пользователя.
        /// </summary>
        public decimal Wages { get; set; }

        /// <summary>
        /// Активность пользователя (например, если он активен в системе).
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Дата рождения пользователя.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Полное имя пользователя (составляется как Name + SecondName).
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Идентификатор профессии пользователя.
        /// </summary>
        public int? ProfessionId { get; set; }

        /// <summary>
        /// Профессия пользователя.
        /// </summary>
        public Profession Profession { get; set; }

        /// <summary>
        /// Список ролей, привязанных к пользователю.
        /// </summary>
        public List<RolesUsers> Roles { get; set; }
    }
}
