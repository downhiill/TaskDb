using _1.Commands;
using Project.Data;  // Для использования ApplicationContext
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;  // Для DI контейнера
using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Project.IService;

namespace _1
{
    public class App
    {
        private readonly ServiceUser _service;

        public App()
        {
            // Создаем конфигурацию из appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            // Настроим DI контейнер и передадим конфигурацию
            var serviceProvider = new ServiceCollection()
                .AddSingleton(configuration)  // Добавляем конфигурацию в DI
                .AddDbContext<ApplicationContext>((sp, options) =>
                    options.UseSqlServer(sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection")))
                .AddScoped<IServiceUsers, ServiceUser>() // Регистрируем ServiceUser как IServiceUsers
                .BuildServiceProvider();

            // Получаем экземпляр интерфейса IServiceUsers из DI
            _service = (ServiceUser?)serviceProvider.GetRequiredService<IServiceUsers>();  // Используем интерфейс, а не конкретный класс
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
