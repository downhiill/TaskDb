using System;

namespace Project.IService
{
    /// <summary>
    /// Модель пользователя, содержащая информацию о пользователе.
    /// Используется для передачи данных о пользователе в приложении.
    /// </summary>
    public class UserModel
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
        /// Получает или устанавливает фамилию пользователя.
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Получает или устанавливает полное имя пользователя.
        /// Формируется как комбинация имени и фамилии.
        /// </summary>
        public string FullName { get; set; }

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
        /// Если значение равно <c>true</c>, то пользователь активен.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Получает или устанавливает дату рождения пользователя.
        /// Может быть <c>null</c>, если дата не указана.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор роли пользователя.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Получает или устанавливает модель роли пользователя.
        /// </summary>
        public RoleModel RoleModel { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор профессии пользователя.
        /// </summary>
        public int? ProfessionId { get; set; }

        /// <summary>
        /// Получает или устанавливает модель профессии пользователя.
        /// </summary>
        public ProfessionModel ProfessionModel { get; set; }
    }
}
