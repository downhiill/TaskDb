using _1.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace _1.Models.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="RolesUsers"/> для настройки модели базы данных.
    /// </summary>
    public class RolesUserConfiguration : IEntityTypeConfiguration<RolesUsers>
    {
        /// <summary>
        /// Метод конфигурации для сущности <see cref="RolesUsers"/>.
        /// </summary>
        /// <param name="builder">Объект для построения конфигурации сущности.</param>
        public void Configure(EntityTypeBuilder<RolesUsers> builder)
        {
            // Указываем имя таблицы
            builder.ToTable("RolesUsers");

            // Определяем составной первичный ключ
            builder.HasKey(ru => new { ru.UserId, ru.RoleId });

            // Настройка связи с сущностью User
            builder.HasOne(ru => ru.User)
                   .WithMany(u => u.Roles) // Пользователь может иметь несколько ролей
                   .HasForeignKey(ru => ru.UserId) // Внешний ключ для связи с таблицей пользователей
                   .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление

            // Настройка связи с сущностью Role
            builder.HasOne(ru => ru.Role)
                   .WithMany(r => r.Users) // Роль может быть у нескольких пользователей
                   .HasForeignKey(ru => ru.RoleId) // Внешний ключ для связи с таблицей ролей
                   .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление
        }
    }
}
