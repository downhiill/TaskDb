using _1.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Models.Configuration
{
    public class RolesUserConfiguration : IEntityTypeConfiguration<RolesUsers>
    {
        public void Configure(EntityTypeBuilder<RolesUsers> builder)
        {
            // Задаем имя таблицы
            builder.ToTable("RolesUsers");

            // Определяем первичный ключ
            builder.HasKey(ru => new { ru.UserId, ru.RoleId });

            // Настройка внешнего ключа для связи с User
            builder.HasOne(ru => ru.User)
                   .WithMany(u => u.Roles)
                   .HasForeignKey(ru => ru.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление

            // Настройка внешнего ключа для связи с Roles
            builder.HasOne(ru => ru.Role)
                   .WithMany(r => r.Users)
                   .HasForeignKey(ru => ru.RoleId)
                   .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление
        }
    }
}
