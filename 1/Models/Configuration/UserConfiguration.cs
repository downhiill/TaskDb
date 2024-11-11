using _1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1
{
    class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
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
