using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    /// <summary>
    /// Модель пользователя с профессией.
    /// </summary>
    public class ModelUserProfession
    {
        /// <summary>
        /// Получает или устанавливает имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Получает или устанавливает название профессии пользователя.
        /// </summary>
        public string ProfessionName { get; set; }
    }
}
