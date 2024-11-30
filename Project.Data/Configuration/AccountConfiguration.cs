using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Project.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Account"/> для настройки модели базы данных.
    /// </summary>
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Account"/> для модели базы данных.
        /// </summary>
        /// <param name="builder">Построитель для конфигурации сущности.</param>
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // Установка UserId как ключа для Account
            builder.HasKey(a => a.UserId);

            // Установка отношения "один-к-одному" с таблицей Users
            builder
                .HasOne(a => a.UserDb)
                .WithOne(u => u.Account)
                .HasForeignKey<Account>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Удаление User удаляет связанный Account

            // Дополнительная настройка свойств
            builder.Property(a => a.Login).IsRequired();
            builder.Property(a => a.Password).IsRequired();
        }
    }
}
