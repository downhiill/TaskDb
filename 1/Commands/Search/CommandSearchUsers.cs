using System;
using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.Search
{
    /// <summary>
    /// Команда для поиска пользователей по имени.
    /// </summary>
    public class CommandSearchUsers : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandSearchUsers"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandSearchUsers(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Поиск по имени";

        /// <summary>
        /// Выполняет команду поиска пользователей по части имени.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя часть имени для поиска, выполняет поиск пользователей по этому имени
        /// и выводит результаты. В случае отсутствия пользователей, выводится сообщение об этом.
        /// </remarks>
        public void Execute()
        {
            // Запрос части имени для поиска
            Console.Write("Введите часть имени пользователя для поиска: ");
            string term = Console.ReadLine();

            // Используем метод для поиска пользователей по имени
            var users = _service.SearchUsers(term);

            if (users.Count > 0)
            {
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
