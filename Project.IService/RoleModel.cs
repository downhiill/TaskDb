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
            /// Уникальный идентификатор роли, основанный на перечислении <see cref="EnumTypeRoles"/>.
            /// </summary>
            public EnumTypeRoleModel Id { get; set; }

            /// <summary>
            /// Название роли (например, "Админ", "Пользователь", "Гость").
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Список пользователей, которые имеют эту роль.
            /// </summary>
            public List<RolesUsersModel> Users { get; set; }

    }
}
