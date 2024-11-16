using _1.Models.Entities;
using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1.Commands.ShowAll
{
    /// <summary>
    /// Команда для получения всех ролей заданного пользователя.
    /// </summary>
    public class CommandGetRolesUser : ICommand
    {
        private readonly ServiceRoles _serviceRoles;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandGetRolesUser"/>.
        /// </summary>
        /// <param name="serviceRoles">Сервис для работы с ролями пользователей.</param>
        public CommandGetRolesUser(ServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles ?? throw new ArgumentNullException(nameof(serviceRoles), "Сервис ролей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Получаем список всех ролей заданного пользователя";

        /// <summary>
        /// Выполняет команду, отображающую список ролей для заданного пользователя.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя ID и получает список ролей для пользователя с этим ID.
        /// Если роли найдены, они выводятся на экран. Если ролей нет, выводится соответствующее сообщение.
        /// </remarks>
        public void Execute()
        {
            // Запрашиваем у пользователя ID
            Console.WriteLine("Введите ID пользователя:");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Некорректный ID пользователя.");
                return;
            }

            // Получаем список ролей для данного пользователя
            var roles = _serviceRoles.GetRolesUser(userId);

            if (roles.Any())
            {
                // Если роли найдены, выводим их
                Console.WriteLine($"Роли для пользователя с ID {userId}:");
                foreach (var role in roles)
                {
                    Console.WriteLine($"Роль: {role.Name}");
                }
            }
            else
            {
                // Если роли не найдены
                Console.WriteLine($"Пользователь с ID {userId} не имеет ролей.");
            }
        }
    }
}
