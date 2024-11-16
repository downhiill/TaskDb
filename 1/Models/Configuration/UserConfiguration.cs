using _1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace _1.Models.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="User"/> для работы с Entity Framework.
    /// </summary>
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Настроить сущность <see cref="User"/> в контексте Entity Framework.
        /// </summary>
        /// <param name="builder">Строитель сущности для <see cref="User"/>.</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Настройка поля Name: обязательное и максимальная длина 30 символов
            builder.Property(u => u.Name)
                .IsRequired() // Поле обязательно
                .HasMaxLength(30); // Максимальная длина 30 символов

            // Настройка поля DateOfBirth: тип данных datetime2
            builder.Property(u => u.DateOfBirth)
                .HasColumnType("datetime2"); // Настройка типа данных для поля

            // Настройка поля DateCreate: по умолчанию текущая дата и время
            builder.Property(u => u.DateCreate)
                .HasDefaultValueSql("GETDATE()"); // Значение по умолчанию - текущая дата

            // Настройка вычисляемого поля FullName, которое соединяет поля Name и SecondName
            builder.Property(u => u.FullName)
                .HasComputedColumnSql("[Name] + ' ' + [SecondName]"); // Строковое вычисление полного имени
        }
    }
}
