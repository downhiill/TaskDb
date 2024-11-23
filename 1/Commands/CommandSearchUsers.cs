using Project.IService;
using System;

namespace _1.Commands
{
    /// <summary>
    /// Команда для поиска пользователей по части имени.
    /// </summary>
    public class CommandSearchUsers : ICommand
    {
        private readonly IServiceUsers _service;

        /// <summary>
        /// Конструктор, инициализирующий сервис пользователей.
        /// </summary>
        /// <param name="service">Интерфейс сервиса для работы с пользователями.</param>
        public CommandSearchUsers(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Название команды, отображаемое в меню.
        /// </summary>
        public string Name => "Поиск по имени";

        /// <summary>
        /// Выполняет поиск пользователей по части имени.
        /// Запрашивает поисковый термин, выполняет поиск и выводит результаты.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите часть имени пользователя для поиска: ");
            string term = Console.ReadLine()?.Trim();

            // Проверка на пустую строку
            if (string.IsNullOrEmpty(term))
            {
                Console.WriteLine("Поисковая строка не может быть пустой. Попробуйте снова.");
                return;
            }

            // Выполняем поиск
            var users = _service.SearchUsers(term);

            // Выводим результаты
            if (users.Count > 0)
            {
                Console.WriteLine("Найденные пользователи:");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Возраст: {user.Age}");
                }
            }
            else
            {
                Console.WriteLine("Пользователи не найдены.");
            }
        }
    }
}
