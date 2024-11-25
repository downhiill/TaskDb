using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.IService
{
    /// <summary>
    /// Представляет пользователя с краткой информацией.
    /// </summary>
    public class ShortUser
    {
        /// <summary>
        /// Получает или устанавливает уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает дату рождения пользователя. Может быть null, если дата не указана.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
    }
}
