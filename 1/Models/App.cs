using _1.Models.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace _1.Models
{
    public class App
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Run()
        {
            // Получаем все типы команд, которые реализуют интерфейс ICommand
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
                    var command = (ICommand)_serviceProvider.GetRequiredService(commandType); // Разрешаем команду через DI
                    Console.WriteLine($"{i + 1}. {command.Name}");
                }

                var choice = Console.ReadLine();

                // Преобразуем выбор в индекс и выполняем команду
                if (int.TryParse(choice, out int index) && index >= 1 && index <= commandTypes.Count)
                {
                    var commandType = commandTypes[index - 1];
                    var command = (ICommand)_serviceProvider.GetRequiredService(commandType); // Разрешаем команду через DI
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
