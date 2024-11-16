using System;
using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.Delete
{
    /// <summary>
    /// Команда для удаления профессии.
    /// </summary>
    internal class CommandDeleteProfession : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandDeleteProfession"/>.
        /// </summary>
        /// <param name="service">Сервис, отвечающий за управление профессиями.</param>
        public CommandDeleteProfession(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Удалить профессию";

        /// <summary>
        /// Выполняет команду удаления профессии.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID профессии и вызывает сервис для удаления профессии.
        /// </remarks>
        public void Execute()
        {
            Console.Write("Введите ID профессии для удаления: ");

            if (!int.TryParse(Console.ReadLine(), out int professionId))
            {
                Console.WriteLine("Некорректный ID профессии.");
                return;
            }

            _service.DeleteProfession(professionId);
            Console.WriteLine("Профессия удалена.");
        }
    }
}
