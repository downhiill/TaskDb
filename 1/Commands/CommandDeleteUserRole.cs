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
    public class CommandDeleteUserRole : ICommand
    {
        private readonly IServiceRoles _serviceRoles;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandDeleteUserRole"/>.
        /// </summary>
        /// <param name="serviceRoles">Сервис для работы с ролями пользователей.</param>
        public CommandDeleteUserRole(IServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles ?? throw new ArgumentNullException(nameof(serviceRoles), "Сервис ролей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Удаляем роль пользователю";

        /// <summary>
        /// Выполняет команду удаления роли у пользователя.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID и имя роли, а затем вызывает сервис для удаления указанной роли у пользователя.
        /// </remarks>
        public void Execute()
        {
            // Запрашиваем у пользователя данные для удаления роли
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
                // Удаляем роль у пользователя
                _serviceRoles.UserRemoveRole(userId, role);
                Console.WriteLine($"Роль {role} успешно удалена у пользователя с ID {userId}.");
            }
            else
            {
                Console.WriteLine("Некорректное имя роли.");
            }
        }
    }
}
