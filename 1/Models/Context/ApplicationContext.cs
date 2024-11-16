using _1.Models.Configuration;
using _1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace _1.Models.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Profession> Professions { get; set; }

        private readonly IConfiguration _configuration;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Убедитесь, что база данных создана
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration()); // Применение конфигурации для User
            modelBuilder.ApplyConfiguration(new RoleConfiguration()); // Применение конфигурации для Role
            modelBuilder.ApplyConfiguration(new ProfessionConfiguration());
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
