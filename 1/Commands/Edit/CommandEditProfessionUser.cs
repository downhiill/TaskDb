using _1.Models.Interface;
using System;
using static _1.Services.ServiceUser;

namespace _1.Commands.Edit
{
    /// <summary>
    /// Команда для изменения профессии пользователя.
    /// </summary>
    public class CommandEditProfessionUser : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandEditProfessionUser"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandEditProfessionUser(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Изменить профессию";

        /// <summary>
        /// Выполняет команду изменения профессии пользователя.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID и новую профессию, а затем вызывает сервис для изменения профессии указанного пользователя.
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

            // Запрашиваем новую профессию пользователя
            Console.Write("Введите новую профессию пользователя: ");
            if (!int.TryParse(Console.ReadLine(), out int professionId))
            {
                Console.WriteLine("Некорректный ID профессии.");
                return;
            }

            // Вызываем сервис для изменения профессии пользователя
            _service.EditProfessionUser(userId, professionId);
            Console.WriteLine("Профессия пользователя изменена.");
        }
    }
}
