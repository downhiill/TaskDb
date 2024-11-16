using System;
using _1.Services;
using static _1.Services.ServiceUser;

namespace _1.Commands.Edit
{
    /// <summary>
    /// Команда для изменения заработной платы пользователя.
    /// </summary>
    internal class CommandEditWages
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandEditWages"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandEditWages(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Изменить З/п";

        /// <summary>
        /// Выполняет команду изменения заработной платы пользователя.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID и новую заработную плату, а затем вызывает сервис для изменения данных.
        /// </remarks>
        public void Execute()
        {
            // Запрашиваем у пользователя ID пользователя
            Console.Write("Введите ID пользователя: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Некорректный ID пользователя.");
                return;
            }

            // Запрашиваем у пользователя новую зарплату
            Console.Write("Введите новую заработную плату: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal wages))
            {
                Console.WriteLine("Некорректная сумма заработной платы.");
                return;
            }

            // Изменяем заработную плату пользователя
            _service.EditWages(userId, wages);
            Console.WriteLine("Зарплата пользователя изменена.");
        }
    }
}
