using _1.Models;
using _1.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using static _1.Services.ServiceUser;

namespace _1
{
    public class App
    {
        private ApplicationContext _dbContext;
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр приложения.
        /// </summary>
        public App(ApplicationContext dbContext)
        {
            // Инжектируем DbContext через конструктор
            _dbContext = dbContext;

            // Инициализируем сервис пользователей
            _service = new ServiceUsers(_dbContext);
        }

        public void Run()
        {
            // Получаем все команды, которые реализуют интерфейс ICommand
            var commandTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface)
                .ToList();

            // Выводим меню с доступными командами
            while (true)
            {
                Console.WriteLine("Выберите действие:");

                // Динамически выводим меню
                for (int i = 0; i < commandTypes.Count; i++)
                {
                    var commandType = commandTypes[i];
                    var command = (ICommand)Activator.CreateInstance(commandType, _service);
                    Console.WriteLine($"{i + 1}. {command.Name}");
                }

                var choice = Console.ReadLine();

                // Преобразуем выбор в индекс и выполняем команду
                if (int.TryParse(choice, out int index) && index >= 1 && index <= commandTypes.Count)
                {
                    var commandType = commandTypes[index - 1];
                    var command = (ICommand)Activator.CreateInstance(commandType, _service);
                    command.Execute();
                }
                else
                {
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                }
            }
        }
    }
}