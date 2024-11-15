using static _1.Services.ServiceUser;

namespace _1.Commands.ShowAll
{
    /// <summary>
    /// Команда для вывода всех пользователей с пагинацией.
    /// </summary>
    public class CommandShowAllUsers : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр команды для вывода всех пользователей.
        /// </summary>
        /// <param name="service">Сервис пользователей, который используется для получения всех пользователей.</param>
        public CommandShowAllUsers(ServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Получает название команды.
        /// </summary>
        public string Name => "Вывод всех пользователей";

        /// <summary>
        /// Выполняет команду вывода всех пользователей с пагинацией.
        /// По умолчанию выводит первых 10 пользователей.
        /// </summary>
        public void Execute()
        {
            // Пагинация: пропускаем 0 пользователей, берем 10
            ShowPagedUsers(0, 10);
        }

        /// <summary>
        /// Логика с пагинацией для вывода пользователей.
        /// </summary>
        /// <param name="skip">Количество пропущенных пользователей.</param>
        /// <param name="take">Количество пользователей для вывода.</param>
        public void ShowPagedUsers(int skip, int take)
        {
            // Получаем всех пользователей
            var users = _service.GetAllUsers();
            var selectedUsers = users.Skip(skip).Take(take).ToList();

            if (selectedUsers.Count > 0)
            {
                foreach (var user in selectedUsers)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Зарплата: {user.Wages}, Возраст: {user.Age}");
                }
            }
            else
            {
                Console.WriteLine("Пользователи не найдены.");
            }
        }
    }
}
