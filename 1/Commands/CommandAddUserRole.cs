using Project.Data;
using Project.IService;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands
{
    /// <summary>
    /// Команда для добавления роли пользователю.
    /// </summary>
    public class CommandAddUserRole : ICommand
    {
        private readonly IServiceRoles _serviceRoles;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandAddUserRole"/>.
        /// </summary>
        /// <param name="serviceRoles">Сервис, отвечающий за добавление ролей пользователям.</param>
        public CommandAddUserRole(IServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles ?? throw new ArgumentNullException(nameof(serviceRoles), "Сервис не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Добавляем роль пользователю";

        /// <summary>
        /// Выполняет команду добавления роли пользователю.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID и роль, а затем добавляет роль пользователю через сервис.
        /// </remarks>
        public void Execute()
        {
            // Запрашиваем у пользователя данные для добавления роли
            Console.WriteLine("Введите ID пользователя:");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Некорректный ID пользователя.");
                return;
            }

            Console.WriteLine("Введите роль (например, Admin, User, Manager):");
            string roleName = Console.ReadLine();

            // Преобразуем строку в Enum
            if (Enum.TryParse(roleName, true, out EnumTypeRoleModel role))
            {
                // Добавляем роль пользователю
                _serviceRoles.UserAddRole(userId, role);
                Console.WriteLine($"Роль {role} успешно добавлена пользователю с ID {userId}.");
            }
            else
            {
                Console.WriteLine("Некорректное имя роли.");
            }
        }
    }
}
