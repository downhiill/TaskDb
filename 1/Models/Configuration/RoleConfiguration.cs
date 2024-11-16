using _1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace _1.Models.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Roles"/> для настройки модели базы данных.
    /// </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<Roles>
    {
        /// <summary>
        /// Метод конфигурации для сущности <see cref="Roles"/>.
        /// </summary>
        /// <param name="builder">Объект для построения конфигурации сущности.</param>
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            // Указываем, что свойство Name обязательно для заполнения и ограничиваем его максимальной длиной
            builder.Property(r => r.Name)
                .IsRequired() // Поле обязательно
                .HasMaxLength(30); // Максимальная длина 30 символов

            // Инициализация данных для таблицы Roles
            builder.HasData(
                new Roles { Id = EnumTypeRoles.User, Name = "Пользователь" },
                new Roles { Id = EnumTypeRoles.Admin, Name = "Админ" },
                new Roles { Id = EnumTypeRoles.Guest, Name = "Гость" }
            );
        }
    }
}
