using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    /// <summary>
    /// Класс, представляющий краткое описание роли.
    /// </summary>
    public class ShortRole
    {
        /// <summary>
        /// Тип роли, представленный значением перечисления <see cref="EnumTypeRoles"/>.
        /// </summary>
        public EnumTypeRoleModel Type { get; set; }

        /// <summary>
        /// Название роли.
        /// </summary>
        public string Name { get; set; }
    }
}
