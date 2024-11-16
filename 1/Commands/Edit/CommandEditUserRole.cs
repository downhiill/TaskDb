using _1.Models.Entities;
using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1.Commands.Edit
{
    /// <summary>
    /// Команда для изменения роли пользователя.
    /// </summary>
    public class CommandEditUserRole : ICommand
    {
        private readonly ServiceRoles _serviceRoles;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandEditUserRole"/>.
        /// </summary>
        /// <param name="serviceRoles">Сервис для работы с ролями пользователей.</param>
        public CommandEditUserRole(ServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles ?? throw new ArgumentNullException(nameof(serviceRoles), "Сервис ролей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Изменить роль у пользователя";

        /// <summary>
        /// Выполняет команду изменения роли пользователя.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID пользователя и новые роли, а затем вызывает сервис для изменения ролей.
        /// </remarks>
        public void Execute()
        {
            // Запрашиваем у пользователя ID пользователя, чьи роли нужно изменить
            Console.WriteLine("Введите ID пользователя, чьи роли нужно изменить:");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Некорректный ID пользователя.");
                return;
            }

            // Запрашиваем у пользователя новые роли для пользователя
            Console.WriteLine("Введите роли через запятую (например, Admin, User):");
            string inputRoles = Console.ReadLine();

            // Преобразуем строку в список ролей
            var roleNames = inputRoles.Split(',').Select(r => r.Trim()).ToList();

            // Преобразуем роли в Enum
            var roles = new List<EnumTypeRoles>();
            foreach (var roleName in roleNames)
            {
                if (Enum.TryParse(roleName, true, out EnumTypeRoles role))
                {
                    roles.Add(role);
                }
                else
                {
                    Console.WriteLine($"Некорректная роль: {roleName}");
                }
            }

            if (roles.Count > 0)
            {
                // Изменяем роли пользователя
                _serviceRoles.UserChangeRole(userId, roles);
                Console.WriteLine($"Роли пользователя с ID {userId} успешно обновлены.");
            }
            else
            {
                Console.WriteLine("Не удалось преобразовать введенные роли.");
            }
        }
    }
}
