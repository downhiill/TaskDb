using Microsoft.EntityFrameworkCore;
using Project.Data;

public class ApplicationContext : DbContext
{
    public DbSet<UserDb> Users { get; set; }

    // Конструктор для использования DI
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);  // DI будет управлять конфигурацией
    }
}
