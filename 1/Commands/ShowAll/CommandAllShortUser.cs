using _1.Services;
using System;
using System.Collections.Generic;
using _1.Models.Entities;
using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.ShowAll
{
    /// <summary>
    /// Команда для отображения краткой информации о пользователях.
    /// </summary>
    public class CommandShowShortUsers : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandShowShortUsers"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandShowShortUsers(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Вывод краткой информации о пользователях";

        /// <summary>
        /// Выполняет команду, отображающую краткую информацию о пользователях.
        /// </summary>
        /// <remarks>
        /// Запрашивает сервис для получения списка пользователей с их краткой информацией.
        /// Если пользователей нет, выводится сообщение об этом.
        /// </remarks>
        public void Execute()
        {
            int skip = 0;  // Пропускаем 0 пользователей
            int take = 10; // Берём 10 пользователей

            // Получаем список пользователей с их краткой информацией
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
