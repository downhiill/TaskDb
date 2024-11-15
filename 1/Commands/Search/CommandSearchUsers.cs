using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;
using System.Windows.Input;
using _1.Models;

namespace _1.Commands.Search
{
    /// <summary>
    /// Команда для поиска пользователей по имени.
    /// </summary>
    public class CommandSearchUsers : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр команды поиска пользователей по имени.
        /// </summary>
        /// <param name="service">Сервис пользователей, который используется для выполнения поиска.</param>
        public CommandSearchUsers(ServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Получает название команды.
        /// </summary>
        public string Name => "Поиск по имени";

        /// <summary>
        /// Выполняет команду поиска пользователей по имени. Запрашивает часть имени для поиска
        /// и выводит список пользователей, чьи имена содержат указанную подстроку.
        /// </summary>
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
