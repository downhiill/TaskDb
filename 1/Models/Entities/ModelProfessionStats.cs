using System;

namespace _1.Models.Entities
{
    /// <summary>
    /// Модель статистики профессии, содержащая имя профессии и количество пользователей с данной профессией.
    /// </summary>
    public class ModelProfessionStats
    {
        /// <summary>
        /// Имя профессии.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество пользователей, связанных с данной профессией.
        /// </summary>
        public int Count { get; set; }
    }
}
