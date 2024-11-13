using _1.Models.Configuration;
using _1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace _1.Models.Context
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RolesUsers> RolesUsers { get; set; }
        public DbSet<Profession> Professions { get; set; }

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
            modelBuilder.ApplyConfiguration(new UserConfiguration()); 
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolesUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProfessionConfiguration());
        }
    }
}
