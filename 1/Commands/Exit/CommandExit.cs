using System;
using _1.Models.Interface;
using _1.Services;
using static _1.Services.ServiceUser;

namespace _1.Commands.Exit
{
    /// <summary>
    /// Команда для выхода из программы.
    /// </summary>
    public class CommandExit : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandExit"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями (не используется в данном классе, но передается для совместимости).</param>
        public CommandExit(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Выход";

        /// <summary>
        /// Выполняет команду выхода из программы.
        /// </summary>
        /// <remarks>
        /// Завершается выполнение программы с помощью метода <see cref="Environment.Exit(int)"/>.
        /// </remarks>
        public void Execute()
        {
            Console.WriteLine("Выход из программы...");
            Environment.Exit(0);  // Завершаем выполнение программы
        }
    }
}
