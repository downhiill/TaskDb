using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Configuration
{
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            // Настройка ключа
            builder.HasKey(u => u.Id);

            // Установка отношения "один-к-одному" с таблицей Users
            builder
                .HasOne(ui => ui.UserDb)
                .WithOne(u => u.Info)
                .HasForeignKey<UserInfo>(ui => ui.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Удаление User удалит связанные записи в UserInfo

            // Дополнительная настройка свойств
            builder.Property(ui => ui.DateCreate).IsRequired();
            builder.Property(ui => ui.Age).IsRequired();
        }
    }
}
