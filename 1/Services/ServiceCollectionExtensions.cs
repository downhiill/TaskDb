using _1.Models.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using static _1.Services.ServiceUser;

namespace _1.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            // Получаем все типы из текущей сборки, которые реализуют ICommand
            var commandTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface)
                .ToList();

            // Регистрируем каждую команду
            foreach (var commandType in commandTypes)
            {
                // Получаем конструктор команды
                var constructor = commandType.GetConstructors().FirstOrDefault();

                // Получаем параметры конструктора
                var constructorParameters = constructor?.GetParameters();

                if (constructorParameters != null)
                {
                    // Если конструктор имеет зависимости, пытаемся зарегистрировать их
                    foreach (var parameter in constructorParameters)
                    {
                        if (parameter.ParameterType == typeof(ServiceUsers))
                        {
                            services.AddScoped<ServiceUsers>();
                        }
                        else if (parameter.ParameterType == typeof(ServiceRoles))
                        {
                            services.AddScoped<ServiceRoles>();
                        }
                    }
                }

                // Регистрируем команду как сервис в DI контейнере
                services.AddScoped(commandType); // Исправлено, теперь регистрируем по типу
            }

            return services;
        }

    }
}
