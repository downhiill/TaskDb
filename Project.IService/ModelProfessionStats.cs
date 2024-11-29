using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    /// <summary>
    /// Модель статистики по профессиям.
    /// </summary>
    public class ModelProfessionStats
    {
        /// <summary>
        /// Получает или устанавливает название профессии.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает количество пользователей, относящихся к данной профессии.
        /// </summary>
        public int Count { get; set; }
    }
}
