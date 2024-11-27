using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project.Data;

namespace Project.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="UserDb"/>.
    /// Определяет правила валидации и ограничения для полей таблицы пользователей.
    /// </summary>
    class UserConfiguration : IEntityTypeConfiguration<UserDb>
    {
        /// <summary>
        /// Конфигурирует свойства и отношения сущности <see cref="UserDb"/>.
        /// </summary>
        /// <param name="builder">
        /// Объект <see cref="EntityTypeBuilder{TEntity}"/>, используемый для настройки сущности.
        /// </param>
        public void Configure(EntityTypeBuilder<UserDb> builder)
        {
            // Настройка поля Name: обязательное и максимальная длина 30 символов
            builder.Property(u => u.Name)
                .IsRequired() // Поле обязательно для заполнения
                .HasMaxLength(30); // Максимальная длина - 30 символов

            // Настройка поля DateOfBirth: тип данных datetime2
            builder.Property(u => u.DateOfBirth)
                .HasColumnType("datetime2"); // Указание точного типа данных в базе
        }
    }
}
