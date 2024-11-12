using _1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models.Configuration
{
    public class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> builder)
        {
            // Задаем имя таблицы
            builder.ToTable("Professions");

            // Указываем первичный ключ
            builder.HasKey(p => p.Id);

            // Указываем связь с коллекцией пользователей
            builder.HasMany(p => p.Users)
                   .WithOne(u => u.Profession)
                   .HasForeignKey(u => u.ProfessionId)
                   .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление
        }
    }
}
