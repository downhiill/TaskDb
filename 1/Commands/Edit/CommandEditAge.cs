using _1.Models.Interface;
using static _1.Services.ServiceUser;
using System;

namespace _1.Commands.Edit
{
    internal class CommandEditAge : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandEditAge"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandEditAge(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Изменить возраст";

        /// <summary>
        /// Выполняет команду изменения возраста пользователя.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID и новый возраст, а затем вызывает сервис для изменения возраста указанного пользователя.
        /// </remarks>
        public void Execute()
        {
            // Запрашиваем у пользователя ID пользователя
            Console.Write("Введите ID пользователя: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Некорректный ID пользователя.");
                return;
            }

            // Запрашиваем новый возраст
            Console.Write("Введите новый возраст пользователя: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Некорректный возраст.");
                return;
            }

            // Вызываем сервис для изменения возраста пользователя
            _service.EditAge(userId, age);
            Console.WriteLine("Возраст пользователя изменен.");
        }
    }
}
