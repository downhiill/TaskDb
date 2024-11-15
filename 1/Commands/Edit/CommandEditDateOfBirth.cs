using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;

namespace _1.Commands.Edit
{
    /// <summary>
    /// Команда для изменения даты рождения пользователя.
    /// </summary>
    internal class CommandEditDateOfBirth
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр команды изменения даты рождения.
        /// </summary>
        /// <param name="service">Сервис пользователей, который будет использоваться для изменения даты рождения.</param>
        public CommandEditDateOfBirth(ServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Получает название команды.
        /// </summary>
        public string Name => "Изменить дату рождения";

        /// <summary>
        /// Выполняет команду изменения даты рождения пользователя.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine());

            Console.Write("Введите дату рождения: ");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            _service.EditDateOfBirth(userId, dateOfBirth);
            Console.WriteLine("Дата рождения пользователя изменена.");
        }
    }
}
