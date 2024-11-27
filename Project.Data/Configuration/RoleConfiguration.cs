using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project.Data;

namespace Project.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Role"/>.
    /// Определяет правила валидации и ограничения для полей таблицы.
    /// </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        /// <summary>
        /// Конфигурирует свойства и отношения сущности <see cref="Role"/>.
        /// </summary>
        /// <param name="builder">
        /// Объект <see cref="EntityTypeBuilder{TEntity}"/>, используемый для настройки сущности.
        /// </param>
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Устанавливает обязательность поля Name и максимальную длину в 30 символов
            builder.Property(r => r.Name)
                .IsRequired() // Поле обязательно для заполнения
                .HasMaxLength(30); // Максимальная длина - 30 символов
        }
    }
}
