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
    public class RoleConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(30);

            // Инициализация данных для таблицы Roles
            builder.HasData(
                new Roles { Id = EnumTypeRoles.User, Name = "Пользователь" },
                new Roles { Id = EnumTypeRoles.Admin, Name = "Админ" },
                new Roles { Id = EnumTypeRoles.Guest, Name = "Гость" }
            );
        }
    }
}
