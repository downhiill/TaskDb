using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;
using System.Windows.Input;

namespace _1.Commands.Edit
{
    /// <summary>
    /// Команда для изменения имени пользователя.
    /// Реализует интерфейс <see cref="ICommand"/>.
    /// </summary>
    public class CommandEditName : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр команды изменения имени.
        /// </summary>
        /// <param name="service">Сервис пользователей, который будет использоваться для изменения имени.</param>
        public CommandEditName(ServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Получает название команды.
        /// </summary>
        public string Name => "Изменить имя";

        /// <summary>
        /// Выполняет команду изменения имени пользователя.
        /// Запрашивает ID пользователя и новое имя, затем вызывает метод для обновления имени.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine());

            Console.Write("Введите новое имя пользователя: ");
            string name = Console.ReadLine();

            _service.EditName(userId, name);
            Console.WriteLine("Имя пользователя изменено.");
        }

        /// <summary>
        /// Определяет, может ли команда быть выполнена в данный момент.
        /// </summary>
        /// <returns>Возвращает true, если команда может быть выполнена, иначе false.</returns>
        public bool CanExecute(object parameter)
        {
            return true; // В данном примере команда всегда может быть выполнена
        }

        /// <summary>
        /// Событие, уведомляющее об изменении состояния команды.
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}
