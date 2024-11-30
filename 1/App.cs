using _1.Commands;
using Project.Data;  // Для использования ApplicationContext
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;  // Для DI контейнера
using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Project.IService;
using Project.Service;

namespace _1
{
    /// <summary>
    /// Основной класс приложения, который управляет зависимостями и выполнением команд.
    /// </summary>
    public class App
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            // Создаем конфигурацию из appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            // Настроим DI контейнер и передадим конфигурацию
            var serviceProvider = new ServiceCollection()
                .AddSingleton(configuration)
                .AddDbContext<ApplicationContext>((sp, options) =>
                    options.UseSqlServer(sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection")))
                .AddScoped<IServiceUsers, ServiceUser>()
                .AddScoped<IServiceRoles, ServiceRoles>()
                .AddScoped<IServiceProfession, ServiceProfession>()
                .AddScoped<ServiceAccount>();

            // Регистрируем все команды, которые реализуют ICommand
            RegisterCommands(serviceProvider);

            _serviceProvider = serviceProvider.BuildServiceProvider();
        }

        /// <summary>
        /// Автоматически регистрирует все классы, реализующие ICommand
        /// </summary>
        private void RegisterCommands(IServiceCollection serviceCollection)
        {
            // Получаем все типы, которые реализуют ICommand
            var commandTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface)
                .ToList();

            // Регистрируем каждую команду в DI контейнере
            foreach (var commandType in commandTypes)
            {
                serviceCollection.AddScoped(commandType); // Регистрация каждой команды
            }
        }

        public void Run()
        {
            var commandTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface)
                .ToList();

            while (true)
            {
                Console.WriteLine("Выберите действие:");

                for (int i = 0; i < commandTypes.Count; i++)
                {
                    var commandType = commandTypes[i];
                    var command = (ICommand)_serviceProvider.GetRequiredService(commandType); // Используем DI для создания экземпляра
                    Console.WriteLine($"{i + 1}. {command.Name}");
                }

                var choice = Console.ReadLine();

                if (int.TryParse(choice, out int index) && index >= 1 && index <= commandTypes.Count)
                {
                    var commandType = commandTypes[index - 1];
                    var command = (ICommand)_serviceProvider.GetRequiredService(commandType); // Используем DI для выполнения команды
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