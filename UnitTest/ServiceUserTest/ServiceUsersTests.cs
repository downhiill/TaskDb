using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Project.Data;
using Project.IService;
using System;
using UnitTest;
using Xunit;

namespace UnitTest.ServiceUserTest
{
    /// <summary>
    /// Класс, содержащий тесты для сервиса пользователей.
    /// Использует мокированные объекты и In-Memory базу данных для тестирования.
    /// </summary>
    public class ServiceUsersTests
    {
        /// <summary>
        /// Мокированное представление интерфейса IServiceUsers для тестирования.
        /// </summary>
        protected readonly Mock<IServiceUsers> _mockServiceUsers;

        /// <summary>
        /// Сервис-поставщик, создающий и управляемый объектами с помощью внедрения зависимостей.
        /// </summary>
        protected readonly ServiceProvider _serviceProvider;

        /// <summary>
        /// Конструктор для инициализации тестовой среды.
        /// Настроены Mock для сервиса пользователей и In-Memory база данных для тестов.
        /// </summary>
        public ServiceUsersTests()
        {
            // Инициализация мокированного интерфейса и DI контейнера
            _mockServiceUsers = new Mock<IServiceUsers>();

            _serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString())) // Уникальная база данных для каждого теста
                .AddScoped<IServiceUsers, ServiceUser>() // Используем реальный сервис в DI
                .BuildServiceProvider();
        }
    }
}
