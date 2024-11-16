using _1.Models.Context;
using _1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models.Configuration
{
    public class ServiceConfiguration
    {
        public static IServiceProvider ConfigureServices()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            // Регистрация сервисов
            return new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddScoped<ServiceUser>()
                .AddScoped<App>()
                .BuildServiceProvider();
        }
    }
}
