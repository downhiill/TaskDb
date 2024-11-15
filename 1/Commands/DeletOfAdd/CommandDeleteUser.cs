using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;
using System.Windows.Input;

namespace _1.Commands.DeletOfAdd
{
    /// <summary>
    /// Команда для удаления пользователя.
    /// </summary>
    public class CommandDeleteUser : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр команды для удаления пользователя.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandDeleteUser(ServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Имя команды.
        /// </summary>
        public string Name => "Удалить пользователя";

        /// <summary>
        /// Выполняет команду удаления пользователя.
        /// Запрашивает ID пользователя и удаляет его из системы.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите ID пользователя для удаления: ");
            int userId = int.Parse(Console.ReadLine());

            _service.Delete(userId);
            Console.WriteLine("Пользователь удален.");
        }
    }
}
