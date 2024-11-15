using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace _1.Models
{
    /// <summary>
    /// Представляет контекст базы данных приложения.
    /// Обеспечивает доступ к базе данных через свойства DbSet.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Получает или задает DbSet пользователей в базе данных.
        /// </summary>
        public DbSet<User> Users { get; set; }

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ApplicationContext"/> и настраивает подключение к базе данных.
        /// </summary>
        /// <param name="options">Параметры, используемые для настройки DbContext.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Настраиваем конфигурацию для чтения appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            // Убедитесь, что база данных создана
            Database.EnsureCreated();
        }

        /// <summary>
        /// Настраивает подключение к базе данных, если оно еще не настроено.
        /// </summary>
        /// <param name="optionsBuilder">
        /// Построитель, используемый для создания или изменения параметров DbContext.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Если строка подключения не была настроена ранее, извлекаем ее из конфигурации
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
