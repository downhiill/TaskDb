using Project.IService;
using System;

namespace _1.Commands
{
    /// <summary>
    /// Команда для изменения возраста пользователя.
    /// </summary>
    internal class CommandEditAge
    {
        private readonly IServiceUsers _service;

        /// <summary>
        /// Конструктор, инициализирующий сервис пользователей.
        /// </summary>
        /// <param name="service">Интерфейс сервиса для работы с пользователями.</param>
        public CommandEditAge(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Название команды, отображаемое в меню.
        /// </summary>
        public string Name => "Изменить возраст";

        /// <summary>
        /// Выполняет изменение возраста пользователя.
        /// Запрашивает ID пользователя и новый возраст, после чего обновляет данные.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Введите новый возраст пользователя: ");
            int age = int.Parse(Console.ReadLine());

            _service.EditAge(userId, age);
            Console.WriteLine("Возраст пользователя изменён.");
        }
    }
}
