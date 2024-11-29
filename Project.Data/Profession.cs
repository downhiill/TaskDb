using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    /// <summary>
    /// Представляет профессию в системе.
    /// </summary>
    public class Profession
    {
        /// <summary>
        /// Уникальный идентификатор профессии.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название профессии.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коллекция пользователей, которые имеют данную профессию.
        /// </summary>
        public ICollection<UserDb> Users { get; set; } = new List<UserDb>();
    }
}
