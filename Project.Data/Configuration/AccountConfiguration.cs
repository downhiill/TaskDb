using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // Установка UserId как ключа для Account
            builder.HasKey(a => a.UserId);

            // Установка отношения "один-к-одному" с таблицей Users
            builder
                .HasOne(a => a.UserDb)
                .WithOne(u => u.Account)
                .HasForeignKey<Account>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Удаление User удаляет связанный Account

            // Дополнительная настройка свойств
            builder.Property(a => a.Login).IsRequired();
            builder.Property(a => a.Password).IsRequired();
        }
    }
}
