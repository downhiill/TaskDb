using System;
using _1.Services;
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
        /// Инициализирует новый экземпляр <see cref="CommandEditDateOfBirth"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandEditDateOfBirth(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Изменить дату рождения";

        /// <summary>
        /// Выполняет команду изменения даты рождения пользователя.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID и новую дату рождения, а затем вызывает сервис для изменения даты рождения указанного пользователя.
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

            // Запрашиваем новую дату рождения
            Console.Write("Введите дату рождения: ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
            {
                Console.WriteLine("Некорректная дата рождения.");
                return;
            }

            // Вызываем сервис для изменения даты рождения пользователя
            _service.EditDateOfBirth(userId, dateOfBirth);
            Console.WriteLine("Дата рождения пользователя изменена.");
        }
    }
}
