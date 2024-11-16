using System;
using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.Search
{
    /// <summary>
    /// Команда для поиска пользователей по возрасту.
    /// </summary>
    public class CommandSearchUserAge : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandSearchUserAge"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandSearchUserAge(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Поиск по возрасту";

        /// <summary>
        /// Выполняет команду поиска пользователей по минимальному возрасту.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя минимальный возраст, выполняет поиск пользователей старше указанного возраста
        /// и выводит результаты. В случае отсутствия пользователей, выводится сообщение об этом.
        /// </remarks>
        public void Execute()
        {
            // Запрос возраста для поиска
            Console.Write("Введите минимальный возраст для поиска пользователей: ");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.WriteLine("Пожалуйста, введите корректное число.");
            }

            // Используем метод для поиска пользователей старше указанного возраста
            var users = _service.SearchUsersMoreAge(age);

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
