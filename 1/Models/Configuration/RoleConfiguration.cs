using _1.Models;
using _1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(30);

            // Добавление начальных данных
            builder.HasData(
                new Role { Id = 1, Type = EnumTypeRoles.User, Name = "Пользователь" },
                new Role { Id = 2, Type = EnumTypeRoles.Guest, Name = "Гость" },
                new Role { Id = 3, Type = EnumTypeRoles.Admin, Name = "Администратор" }
            );
        }
    }
}
