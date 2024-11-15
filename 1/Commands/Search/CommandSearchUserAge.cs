using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.Models;
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
        /// Инициализирует новый экземпляр команды поиска пользователей по возрасту.
        /// </summary>
        /// <param name="service">Сервис пользователей, который используется для выполнения поиска.</param>
        public CommandSearchUserAge(ServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Получает название команды.
        /// </summary>
        public string Name => "Поиск по возрасту";

        /// <summary>
        /// Выполняет команду поиска пользователей, чей возраст больше указанного.
        /// Запрашивает минимальный возраст и выводит список пользователей, соответствующих критерию.
        /// </summary>
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
