using System;
using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.Delete
{
    /// <summary>
    /// Команда для удаления пользователя.
    /// </summary>
    public class CommandDeleteUser : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandDeleteUser"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandDeleteUser(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Удалить пользователя";

        /// <summary>
        /// Выполняет команду удаления пользователя.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID пользователя и вызывает сервис для удаления пользователя.
        /// </remarks>
        public void Execute()
        {
            Console.Write("Введите ID пользователя для удаления: ");

            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Некорректный ID пользователя.");
                return;
            }

            _service.Delete(userId);
            Console.WriteLine("Пользователь удален.");
        }
    }
}
