
using System;
using System.Collections.Generic;

using Project.IService;

namespace _1.Commands.ShowAll
{
    /// <summary>
    /// Команда для вывода краткой информации о пользователях.
    /// </summary>
    public class CommandShowShortUsers : ICommand
    {
        private readonly IServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр команды для вывода краткой информации о пользователях.
        /// </summary>
        /// <param name="service">Сервис пользователей, который используется для получения списка пользователей.</param>
        public CommandShowShortUsers(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Получает название команды.
        /// </summary>
        public string Name => "Вывод краткой информации о пользователях";

        /// <summary>
        /// Выполняет команду вывода краткой информации о пользователях.
        /// Запрашивает список пользователей с краткой информацией, начиная с пропущенных и
        /// выводит данные о первых 10 пользователях (ID, имя, дата рождения).
        /// </summary>
        public void Execute()
        {
            int skip = 0;  // Пропускаем 0 пользователей
            int take = 10; // Берём 10 пользователей

            // Получаем краткую информацию о пользователях
            List<ShortUser> users = _service.GetAllShortUsers(skip, take);

            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Дата Рождения: {user.DateOfBirth}");
                }
            }
            else
            {
                Console.WriteLine("Пользователи не найдены.");
            }
        }
    }
}