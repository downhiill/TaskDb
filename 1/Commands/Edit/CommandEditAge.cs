using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;

namespace _1.Commands.Edit
{
    /// <summary>
    /// Команда для изменения возраста пользователя.
    /// </summary>
    internal class CommandEditAge
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр команды для изменения возраста пользователя.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandEditAge(ServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Имя команды.
        /// </summary>
        public string Name => "Изменить возраст";

        /// <summary>
        /// Выполняет команду изменения возраста пользователя.
        /// Запрашивает ID пользователя и новый возраст, затем обновляет данные в системе.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Введите новый возраст пользователя: ");
            int age = int.Parse(Console.ReadLine());

            _service.EditAge(userId, age);
            Console.WriteLine("Возраст пользователя изменен.");
        }
    }
}
