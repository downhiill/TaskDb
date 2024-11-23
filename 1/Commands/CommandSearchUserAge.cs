using Project.IService;
using System;

namespace _1.Commands
{
    /// <summary>
    /// Команда для поиска пользователей старше указанного возраста.
    /// </summary>
    public class CommandSearchUserAge : ICommand
    {
        private readonly IServiceUsers _service;

        /// <summary>
        /// Конструктор, инициализирующий сервис пользователей.
        /// </summary>
        /// <param name="service">Интерфейс сервиса для работы с пользователями.</param>
        public CommandSearchUserAge(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Название команды, отображаемое в меню.
        /// </summary>
        public string Name => "Поиск по возрасту";

        /// <summary>
        /// Выполняет поиск пользователей старше указанного возраста.
        /// Запрашивает возраст, выполняет поиск и выводит результаты.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите минимальный возраст для поиска пользователей: ");
            int age;

            // Проверяем корректность ввода возраста
            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.WriteLine("Пожалуйста, введите корректное число.");
            }

            // Выполняем поиск пользователей старше указанного возраста
            var users = _service.SearchUsersMoreAge(age);

            // Выводим результаты
            if (users.Count > 0)
            {
                Console.WriteLine("Найденные пользователи:");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Возраст: {user.Age}");
                }
            }
            else
            {
                Console.WriteLine("Пользователи не найдены.");
            }
        }
    }
}
