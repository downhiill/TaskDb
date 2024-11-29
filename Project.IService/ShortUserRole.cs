using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    /// <summary>
    /// Класс, представляющий краткое описание пользователя и его роли.
    /// </summary>
    public class ShortUserRole
    {
        /// <summary>
        /// Краткое описание пользователя.
        /// </summary>
        public ShortUser User { get; set; }

        /// <summary>
        /// Краткое описание роли пользователя.
        /// </summary>
        public ShortRole Role { get; set; }
    }
}
