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

namespace UnitTest.ServiceRoleTest
{
    public class ServiceRoleTest
    {
        /// <summary>
        /// Мокированное представление интерфейса IServiceUsers для тестирования.
        /// </summary>
        protected readonly Mock<IServiceRoles> _mockServiceRoles;

        /// <summary>
        /// Сервис-поставщик, создающий и управляемый объектами с помощью внедрения зависимостей.
        /// </summary>
        protected readonly ServiceProvider _serviceProvider;

        public ServiceRoleTest()
        {
            // Инициализация мокированного интерфейса и DI контейнера
            _mockServiceRoles = new Mock<IServiceRoles>();

            _serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString())) // Уникальная база данных для каждого теста
                .AddScoped<IServiceRoles, ServiceRoles>() // Используем реальный сервис в DI
                .BuildServiceProvider();
        }
    }
}
