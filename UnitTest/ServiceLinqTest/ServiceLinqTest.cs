using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Project.Data;
using Project.IService;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ServiceLinqTest
{
    public class ServiceLinqTest
    {
        /// <summary>
        /// Мокированное представление интерфейса IServiceUsers для тестирования.
        /// </summary>
        protected readonly Mock<IServiceLinq> _mockServiceLinq;

        /// <summary>
        /// Сервис-поставщик, создающий и управляемый объектами с помощью внедрения зависимостей.
        /// </summary>
        protected readonly ServiceProvider _serviceProvider;

        public ServiceLinqTest()
        {
            // Инициализация мокированного интерфейса и DI контейнера
            _mockServiceLinq = new Mock<IServiceLinq>();

            _serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString())) // Уникальная база данных для каждого теста
                .AddScoped<IServiceLinq, ServiceLinq>() // Используем реальный сервис в DI
                .BuildServiceProvider();
        }
    }
}
