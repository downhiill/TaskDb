using _1.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        // Установка UserId как ключа для Account
        builder.HasKey(a => a.UserId);

        // Установка отношения "один-к-одному" с таблицей Users
        builder
            .HasOne(a => a.User)
            .WithOne(u => u.Account)
            .HasForeignKey<Account>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Удаление User удаляет связанный Account

        // Дополнительная настройка свойств
        builder.Property(a => a.Login).IsRequired();
        builder.Property(a => a.Password).IsRequired();
    }
}