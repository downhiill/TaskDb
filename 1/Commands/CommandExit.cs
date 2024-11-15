using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.Models;
using static _1.Services.ServiceUser;

namespace _1.Commands
{
    /// <summary>
    /// Команда для выхода из программы.
    /// </summary>
    public class CommandExit : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр команды для выхода из программы.
        /// </summary>
        /// <param name="service">Сервис пользователей, который не используется в данной команде, но может быть полезен для расширения.</param>
        public CommandExit(ServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Получает название команды.
        /// </summary>
        public string Name => "Выход";  // Имя команды

        /// <summary>
        /// Выполняет команду выхода из программы.
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("Выход из программы...");
            Environment.Exit(0);  // Завершаем выполнение программы
        }
    }
}
