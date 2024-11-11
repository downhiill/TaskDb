using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace _1.Models
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        private readonly IConfiguration _configuration;

        public ApplicationContext()
        {
            // Настраиваем конфигурацию для чтения appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Извлекаем строку подключения из конфигурации
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration()); // Применение конфигурации для User
            modelBuilder.ApplyConfiguration(new RoleConfiguration()); // Применение конфигурации для Role
            // Устанавливаем значение по умолчанию для поля DateCreate
            modelBuilder.Entity<User>()
                .Property(u => u.DateCreate)
                .HasDefaultValueSql("GETDATE()");

            // Настраиваем вычисляемое поле FullName
            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .HasComputedColumnSql("[Name] + ' ' + [SecondName]");
        }
    }
}
