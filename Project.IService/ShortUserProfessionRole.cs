using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    /// <summary>
    /// Класс, представляющий краткое описание пользователя, его роли и профессии.
    /// </summary>
    public class ShortUserProfessionRole
    {
        /// <summary>
        /// Краткое описание пользователя.
        /// </summary>
        public ShortUser User { get; set; }

        /// <summary>
        /// Краткое описание роли пользователя.
        /// </summary>
        public ShortRole Role { get; set; }

        /// <summary>
        /// Название профессии пользователя.
        /// </summary>
        public string ProfessionName { get; set; }
    }
}
