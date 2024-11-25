using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands.Edit
{
    /// <summary>
    /// Команда для изменения заработной платы пользователя.
    /// </summary>
    internal class CommandEditWages : ICommand
    {
        private readonly IServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр команды изменения заработной платы.
        /// </summary>
        /// <param name="service">Сервис пользователей, который будет использоваться для изменения заработной платы.</param>
        public CommandEditWages(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Получает название команды.
        /// </summary>
        public string Name => "Изменить З/п";

        /// <summary>
        /// Выполняет команду изменения заработной платы пользователя.
        /// Запрашивает ID пользователя и новую заработную плату, затем вызывает метод для обновления данных.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine());

            Console.Write("Введите новую заработную плату: ");
            decimal wages = decimal.Parse(Console.ReadLine());

            _service.EditWages(userId, wages);
            Console.WriteLine("Зарплата пользователя изменена.");
        }
    }
}