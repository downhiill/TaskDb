using _1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace _1.Models.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Profession"/> для настройки модели базы данных.
    /// </summary>
    public class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
    {
        /// <summary>
        /// Метод конфигурации для сущности <see cref="Profession"/>.
        /// </summary>
        /// <param name="builder">Объект для построения конфигурации сущности.</param>
        public void Configure(EntityTypeBuilder<Profession> builder)
        {
            // Задаем имя таблицы
            builder.ToTable("Professions");

            // Указываем первичный ключ
            builder.HasKey(p => p.Id);

            // Указываем связь с коллекцией пользователей
            builder.HasMany(p => p.Users) // Один к многим
                   .WithOne(u => u.Profession) // Каждый пользователь имеет одну профессию
                   .HasForeignKey(u => u.ProfessionId) // Внешний ключ для пользователей
                   .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление
        }
    }
}
