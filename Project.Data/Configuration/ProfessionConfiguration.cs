using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Profession"/> для EF Core.
    /// </summary>
    public class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
    {
        /// <summary>
        /// Настройка конфигурации для сущности <see cref="Profession"/>.
        /// </summary>
        /// <param name="builder">Объект для настройки конфигурации сущности.</param>
        public void Configure(EntityTypeBuilder<Profession> builder)
        {
            // Устанавливаем имя таблицы для сущности
            builder.ToTable("Professions");

            // Устанавливаем ключ для сущности
            builder.HasKey(p => p.Id);

            // Устанавливаем уникальный индекс для поля Name
            builder.HasIndex(p => p.Name).IsUnique();

            // Настройка отношения "Один ко многим" с сущностью User
            builder.HasMany(p => p.Users)
                   .WithOne(u => u.Profession)
                   .HasForeignKey(u => u.ProfessionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
