using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.ShowAll
{
    /// <summary>
    /// Команда для вывода всех пользователей с возможностью пагинации.
    /// </summary>
    public class CommandShowAllUsers : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandShowAllUsers"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandShowAllUsers(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Вывод всех пользователей";

        /// <summary>
        /// Выполняет команду, выводя пользователей с пагинацией (по 10 пользователей за раз).
        /// </summary>
        /// <remarks>
        /// Этот метод вызывает метод <see cref="ShowPagedUsers(int, int)"/> с параметрами для отображения пользователей с пагинацией.
        /// </remarks>
        public void Execute()
        {
            ShowPagedUsers(0, 10); // Передаем параметры пагинации (начало и количество)
        }

        /// <summary>
        /// Показывает пользователей с пагинацией.
        /// </summary>
        /// <param name="skip">Количество пользователей для пропуска (для пагинации).</param>
        /// <param name="take">Количество пользователей для отображения (для пагинации).</param>
        /// <remarks>
        /// Метод получает всех пользователей, применяет пагинацию и выводит информацию о них в консоль.
        /// </remarks>
        public void ShowPagedUsers(int skip, int take)
        {
            var users = _service.GetAllUsers(); // Получаем всех пользователей
            var selectedUsers = users.Skip(skip).Take(take).ToList(); // Применяем пагинацию

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
