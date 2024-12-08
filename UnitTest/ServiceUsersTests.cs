using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Project.Data;
using Project.IService;
using System;
using UnitTest;
using Xunit;

namespace _1.Tests
{
    public class ServiceUsersTests
    {
        protected readonly Mock<IServiceUsers> _mockServiceUsers;
        protected readonly ServiceProvider _serviceProvider;

        public ServiceUsersTests()
        {
            // Инициализация мокированного интерфейса и DI контейнер
            _mockServiceUsers = new Mock<IServiceUsers>();

            _serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString())) // Уникальная база данных для каждого теста
                .AddScoped<IServiceUsers, ServiceUser>() // Используем реальный сервис в DI
                .BuildServiceProvider();
        }
    }
}
