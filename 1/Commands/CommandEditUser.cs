using Project.IService;
using System;

namespace _1.Commands
{
    /// <summary>
    /// Команда для изменения данных пользователя.
    /// </summary>
    public class CommandEditUser : ICommand
    {
        private readonly IServiceUsers _service;

        /// <summary>
        /// Конструктор, инициализирующий сервис пользователей.
        /// </summary>
        /// <param name="service">Интерфейс сервиса для работы с пользователями.</param>
        public CommandEditUser(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Название команды, отображаемое в меню.
        /// </summary>
        public string Name => "Изменить имя";

        /// <summary>
        /// Выполняет изменение имени пользователя.
        /// Запрашивает ID пользователя и новое имя, после чего обновляет данные.
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
    }
}
