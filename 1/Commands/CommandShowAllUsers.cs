using Project.IService;
using System;

namespace _1.Commands
{
    /// <summary>
    /// Команда для отображения всех пользователей.
    /// </summary>
    public class CommandShowAllUsers : ICommand
    {
        private readonly IServiceUsers _service;

        /// <summary>
        /// Конструктор команды для вывода всех пользователей.
        /// </summary>
        /// <param name="service">Интерфейс сервиса для работы с пользователями.</param>
        public CommandShowAllUsers(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Название команды, отображаемое в меню.
        /// </summary>
        public string Name => "Вывод всех пользователей";

        /// <summary>
        /// Выполняет вывод всех пользователей из базы данных.
        /// </summary>
        public void Execute()
        {
            var users = _service.GetAllUsers();

            if (users.Count > 0)
            {
                Console.WriteLine("Список пользователей:");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Возраст: {user.Age}");
                }
            }
            else
            {
                Console.WriteLine("Список пользователей пуст.");
            }
        }
    }
}
