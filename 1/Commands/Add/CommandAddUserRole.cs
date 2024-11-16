using _1.Models.Entities;
using _1.Models.Interface;
using _1.Services;
using System;

namespace _1.Commands.Add
{
    /// <summary>
    /// Команда для добавления роли пользователю.
    /// </summary>
    public class CommandAddUserRole : ICommand
    {
        private readonly ServiceRoles _serviceRoles;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandAddUserRole"/>.
        /// </summary>
        /// <param name="serviceRoles">Сервис, отвечающий за добавление ролей пользователям.</param>
        public CommandAddUserRole(ServiceRoles serviceRoles)
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
            if (Enum.TryParse(roleName, true, out EnumTypeRoles role))
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
