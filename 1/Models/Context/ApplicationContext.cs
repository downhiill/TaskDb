using _1.Models.Configuration;
using _1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace _1.Models.Context
{
    /// <summary>
    /// Контекст базы данных для работы с сущностями пользователей, ролей, профессий и связями между ними.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Представление сущности пользователей в базе данных.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Представление сущности ролей в базе данных.
        /// </summary>
        public DbSet<Roles> Roles { get; set; }

        /// <summary>
        /// Представление сущности связей между ролями и пользователями.
        /// </summary>
        public DbSet<RolesUsers> RolesUsers { get; set; }

        /// <summary>
        /// Представление сущности профессий в базе данных.
        /// </summary>
        public DbSet<Profession> Professions { get; set; }

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApplicationContext"/>.
        /// </summary>
        /// <param name="options">Опции контекста базы данных.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Убедитесь, что база данных создана, если она не существует
            Database.EnsureCreated();
        }

        /// <summary>
        /// Настроить модель базы данных при создании контекста.
        /// </summary>
        /// <param name="modelBuilder">Объект строителя модели для конфигурации сущностей.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Применение конфигурации для сущностей
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolesUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProfessionConfiguration());
        }
    }
}
