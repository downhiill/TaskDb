    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Project.IService
    {
        /// <summary>
        /// Модель профессии, включающая информацию о профессии и пользователях, связанных с ней.
        /// </summary>
        public class ProfessionModel
        {
            /// <summary>
            /// Получает или устанавливает уникальный идентификатор профессии.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Получает или устанавливает название профессии.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Получает или устанавливает коллекцию пользователей, связанных с данной профессией.
            /// </summary>
            public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
        }
    }
