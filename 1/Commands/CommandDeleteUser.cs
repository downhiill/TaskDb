using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _1.Commands
{
    /// <summary>
    /// Команда для удаления пользователя по ID.
    /// Реализует интерфейс ICommand.
    /// </summary>
    public class CommandDeleteUser : ICommand
    {
        private readonly IServiceUsers _service;

        /// <summary>
        /// Конструктор, который инициализирует сервис пользователей.
        /// </summary>
        /// <param name="service">Интерфейс сервиса для работы с пользователями.</param>
        public CommandDeleteUser(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Название команды, отображаемое в меню.
        /// </summary>
        public string Name => "Удалить пользователя";

        /// <summary>
        /// Выполняет удаление пользователя.
        /// Запрашивает ID пользователя, которого нужно удалить.
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
