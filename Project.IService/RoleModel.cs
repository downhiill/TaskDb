using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    /// <summary>
    /// Модель роли, включающая тип роли, название и связь с пользователями.
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// Получает или устанавливает уникальный идентификатор роли.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает тип роли, используя вложенный перечисляемый тип <see cref="EnumTypeRoleModel"/>.
        /// </summary>
        public EnumTypeRoleModel Type { get; set; }

        /// <summary>
        /// Получает или устанавливает название роли.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает коллекцию пользователей, связанных с данной ролью.
        /// </summary>
        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
    }
}
