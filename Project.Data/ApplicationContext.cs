using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.Configuration;

/// <summary>
/// Контекст базы данных для работы с сущностями приложения.
/// Используется для доступа к таблицам пользователей и ролей.
/// </summary>
public class ApplicationContext : DbContext
{
    /// <summary>
    /// Представляет таблицу пользователей в базе данных.
    /// Позволяет выполнять операции CRUD для сущности <see cref="UserDb"/>.
    /// </summary>
    public DbSet<UserDb> Users { get; set; }
    public DbSet<Role> Roles { get; set; }  
    public DbSet<RolesUsers> RolesUsers { get; set; }
    public DbSet<Profession> Professions { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ApplicationContext"/> 
    /// с указанными параметрами конфигурации.
    /// </summary>
    /// <param name="options">Параметры конфигурации контекста базы данных, содержащие настройки подключения и поведения контекста.</param>
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Дополнительная настройка параметров подключения к базе данных.
    /// Этот метод вызывается автоматически при создании контекста.
    /// </summary>
    /// <param name="optionsBuilder">
    /// Объект <see cref="DbContextOptionsBuilder"/>, используемый для настройки параметров контекста.
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder); // DI будет управлять конфигурацией
    }

    /// <summary>
    /// Настройка модели данных для сущностей базы данных.
    /// Этот метод используется для применения конфигураций сущностей, таких как <see cref="UserDb"/> и <see cref="Role"/>.
    /// </summary>
    /// <param name="modelBuilder">
    /// Объект <see cref="ModelBuilder"/>, который используется для настройки сущностей и их свойств.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Применение конфигураций для сущностей User и Role
        modelBuilder.ApplyConfiguration(new UserConfiguration()); // Применение конфигурации для User
        modelBuilder.ApplyConfiguration(new RoleConfiguration()); // Применение конфигурации для Role
        modelBuilder.ApplyConfiguration(new RolesUserConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionConfiguration());

        // Устанавливаем значение по умолчанию для поля DateCreate
        modelBuilder.Entity<UserDb>()
            .Property(u => u.DateCreate)
            .HasDefaultValueSql("GETDATE()");

        // Настроим вычисляемое поле FullName, которое комбинирует имя и фамилию
        modelBuilder.Entity<UserDb>()
            .Property(u => u.FullName)
            .HasComputedColumnSql("[Name] + ' ' + [SecondName]");

    }
}
