using System;

namespace _1.Models.Entities
{
    /// <summary>
    /// Класс, представляющий краткое описание роли.
    /// </summary>
    public class ShortRole
    {
        /// <summary>
        /// Тип роли, представленный значением перечисления <see cref="EnumTypeRoles"/>.
        /// </summary>
        public EnumTypeRoles Type { get; set; }

        /// <summary>
        /// Название роли.
        /// </summary>
        public string Name { get; set; }
    }
}
