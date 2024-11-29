using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Project.IService;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ServiceProfessionTest
{
    public class ServiceProfessionTest
    {
        /// <summary>
        /// Мокированное представление интерфейса IServiceUsers для тестирования.
        /// </summary>
        protected readonly Mock<IServiceProfession> _mockServiceUsers;

        /// <summary>
        /// Сервис-поставщик, создающий и управляемый объектами с помощью внедрения зависимостей.
        /// </summary>
        protected readonly ServiceProvider _serviceProvider;

        public ServiceProfessionTest()
        {
            // Инициализация мокированного интерфейса и DI контейнера
            _mockServiceUsers = new Mock<IServiceProfession>();

            _serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString())) // Уникальная база данных для каждого теста
                .AddScoped<IServiceProfession, ServiceProfession>() // Используем реальный сервис в DI
                .BuildServiceProvider();
        }
    }
}
