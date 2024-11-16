using _1.Models.Context;
using _1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace _1.Models.Configuration
{
    /// <summary>
    /// Конфигурация сервисов приложения и зависимостей.
    /// </summary>
    public class ServiceConfiguration
    {
        /// <summary>
        /// Метод для конфигурации сервисов и DI контейнера.
        /// </summary>
        /// <returns>Возвращает настроенный <see cref="IServiceProvider"/>.</returns>
        public static IServiceProvider ConfigureServices()
        {
            // Создание объекта конфигурации с использованием appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Устанавливаем текущую директорию
                .AddJsonFile("appsettings.json") // Чтение файла конфигурации
                .Build();

            // Регистрация сервисов в DI контейнере
            return new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration) // Регистрация IConfiguration для доступа к конфигурации
                .AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))) // Регистрация контекста БД с строкой подключения из конфигурации
                .AddScoped<ServiceUser>() // Регистрация сервисов для обработки пользователей
                .AddScoped<ServiceRoles>() // Регистрация сервисов для обработки ролей
                .AddScoped<ServiceProfession>() // Регистрация сервисов для обработки профессий
                .AddScoped<App>() // Регистрация основного приложения
                .AddCommands() // Регистрация команд
                .BuildServiceProvider(); // Строим и возвращаем провайдер сервисов
        }
    }
}
