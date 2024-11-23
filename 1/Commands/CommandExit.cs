using Project.IService;
using System;

namespace _1.Commands
{
    /// <summary>
    /// Команда для завершения работы программы.
    /// </summary>
    public class CommandExit : ICommand
    {
        private readonly IServiceUsers _service;

        /// <summary>
        /// Конструктор, инициализирующий сервис пользователей.
        /// </summary>
        /// <param name="service">Интерфейс сервиса для работы с пользователями.</param>
        public CommandExit(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Название команды, отображаемое в меню.
        /// </summary>
        public string Name => "Выход";

        /// <summary>
        /// Завершает выполнение программы.
        /// Выводит сообщение о выходе и завершает выполнение приложения.
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("Выход из программы...");
            Environment.Exit(0); // Завершаем выполнение программы
        }
    }
}
