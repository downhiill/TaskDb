using Microsoft.EntityFrameworkCore;
using Project.Data;

/// <summary>
/// Контекст базы данных для работы с сущностями приложения.
/// </summary>
public class ApplicationContext : DbContext
{
    /// <summary>
    /// Представляет таблицу пользователей в базе данных.
    /// </summary>
    public DbSet<UserDb> Users { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ApplicationContext"/> 
    /// с указанными параметрами конфигурации.
    /// </summary>
    /// <param name="options">Параметры конфигурации контекста базы данных.</param>
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
}
