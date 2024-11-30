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
        /// Получает или устанавливает фамилию пользователя.
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Получает полное имя пользователя, составленное из имени и фамилии.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Получает или устанавливает возраст пользователя.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Получает или устанавливает дату создания записи пользователя.
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
        /// Получает или устанавливает дату рождения пользователя. Может быть <c>null</c>, если дата не указана.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор роли пользователя.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Получает или устанавливает роль пользователя.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор профессии пользователя. Может быть <c>null</c>, если профессия не указана.
        /// </summary>
        public int? ProfessionId { get; set; }

        /// <summary>
        /// Получает или устанавливает профессию пользователя.
        /// </summary>
        public Profession Profession { get; set; }

        /// <summary>
        /// Получает или устанавливает список ролей, привязанных к пользователю.
        /// </summary>
        public List<RolesUsers> Roles { get; set; }

        /// <summary>
        /// Получает или устанавливает дополнительную информацию о пользователе.
        /// </summary>
        public UserInfo Info { get; set; }

        /// <summary>
        /// Получает или устанавливает данные аккаунта пользователя.
        /// </summary>
        public Account Account { get; set; }
    }
}
