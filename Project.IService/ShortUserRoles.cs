using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    public class ShortUserRoles
    {
        /// <summary>
        /// Краткое описание пользователя.
        /// </summary>
        public ShortUser User { get; set; }

        /// <summary>
        /// Список ролей пользователя.
        /// </summary>
        public List<ShortRole> Roles { get; set; }
    }
}
