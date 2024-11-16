using _1.Models.Entities;
using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1.Commands.ShowAll
{
    /// <summary>
    /// Команда для получения списка пользователей определенной роли.
    /// </summary>
    public class CommandGetUsers : ICommand
    {
        private readonly ServiceRoles _serviceRoles;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandGetUsers"/>.
        /// </summary>
        /// <param name="serviceRoles">Сервис для работы с ролями пользователей.</param>
        public CommandGetUsers(ServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles ?? throw new ArgumentNullException(nameof(serviceRoles), "Сервис ролей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Получаем список пользователей определенной роли";

        /// <summary>
        /// Выполняет команду, которая запрашивает роль у пользователя и отображает всех пользователей с указанной ролью.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя роль, преобразует ее в тип <see cref="EnumTypeRoles"/> и затем получает всех пользователей с этой ролью.
        /// Если такие пользователи найдены, они выводятся на экран, иначе выводится сообщение о том, что пользователей с такой ролью нет.
        /// </remarks>
        public void Execute()
        {
            // Запрашиваем у пользователя название роли
            Console.WriteLine("Введите роль пользователя (например, Admin, User, Guest):");
            string roleName = Console.ReadLine();

            // Преобразуем строку в Enum
            if (Enum.TryParse(roleName, true, out EnumTypeRoles role))
            {
                // Получаем пользователей с этой ролью
                var usersWithRole = _serviceRoles.GetUsers(role);

                if (usersWithRole.Any())
                {
                    // Выводим список пользователей
                    Console.WriteLine($"Пользователи с ролью {role}:");

                    foreach (var userRole in usersWithRole)
                    {
                        Console.WriteLine($"ID: {userRole.User.Id}, Name: {userRole.User.Name}, DateOfBirth: {userRole.User.DateOfBirth?.ToString("d")}, Role: {userRole.Role.Name}");
                    }
                }
                else
                {
                    Console.WriteLine($"Нет пользователей с ролью {role}.");
                }
            }
            else
            {
                Console.WriteLine("Некорректное имя роли.");
            }
        }
    }
}
