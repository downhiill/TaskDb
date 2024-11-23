namespace Project.IService
{
    /// <summary>
    /// Модель пользователя, содержащая информацию о пользователе.
    /// </summary>
    public class UserModel
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
        /// Возраст пользователя.
        /// </summary>
        public int Age { get; set; }
    }
}
