using System;
using _1.Services;
using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.Edit
{
    /// <summary>
    /// Команда для изменения имени пользователя.
    /// </summary>
    public class CommandEditName : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandEditName"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandEditName(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Изменить имя";

        /// <summary>
        /// Выполняет команду изменения имени пользователя.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID и новое имя, а затем вызывает сервис для изменения имени указанного пользователя.
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

            // Запрашиваем новое имя пользователя
            Console.Write("Введите новое имя пользователя: ");
            string name = Console.ReadLine();

            // Вызываем сервис для изменения имени пользователя
            _service.EditName(userId, name);
            Console.WriteLine("Имя пользователя изменено.");
        }
    }
}
