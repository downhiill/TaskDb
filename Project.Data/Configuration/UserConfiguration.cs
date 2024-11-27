using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Data;

namespace Project.Data.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<UserDb>
    {
        public void Configure(EntityTypeBuilder<UserDb> builder)
        {
            // Настройка поля Name: обязательное и максимальная длина 30 символов
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(30);

            // Настройка поля DateOfBirth: тип данных datetime2
            builder.Property(u => u.DateOfBirth)
                .HasColumnType("datetime2");

        }
    }
}
