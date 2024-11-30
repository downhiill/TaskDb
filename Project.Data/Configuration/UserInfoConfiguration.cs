using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Project.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="UserInfo"/> для настройки модели базы данных.
    /// </summary>
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        /// <summary>
        /// Настраивает сущность <see cref="UserInfo"/> для модели базы данных.
        /// </summary>
        /// <param name="builder">Построитель для конфигурации сущности.</param>
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            // Настройка ключа
            builder.HasKey(u => u.Id);

            // Установка отношения "один-к-одному" с таблицей Users
            builder
                .HasOne(ui => ui.UserDb)
                .WithOne(u => u.Info)
                .HasForeignKey<UserInfo>(ui => ui.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Удаление User удалит связанные записи в UserInfo

            // Дополнительная настройка свойств
            builder.Property(ui => ui.DateCreate).IsRequired();
            builder.Property(ui => ui.Age).IsRequired();
        }
    }
}
