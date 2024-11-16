using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1.Commands.ShowAll
{
    /// <summary>
    /// Команда для получения пользователей, у которых есть хотя бы одна роль.
    /// </summary>
    public class CommandGetAllUsers : ICommand
    {
        private readonly ServiceRoles _serviceRoles;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandGetAllUsers"/>.
        /// </summary>
        /// <param name="serviceRoles">Сервис для работы с ролями пользователей.</param>
        public CommandGetAllUsers(ServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles ?? throw new ArgumentNullException(nameof(serviceRoles), "Сервис ролей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Получить пользователей у которых есть хотя бы одна роль";

        /// <summary>
        /// Выполняет команду, отображающую список пользователей с хотя бы одной ролью.
        /// </summary>
        /// <remarks>
        /// Запрашивает сервис для получения списка пользователей с их ролями.
        /// Если пользователей с ролями нет, выводится соответствующее сообщение.
        /// </remarks>
        public void Execute()
        {
            // Получаем всех пользователей с хотя бы одной ролью
            var usersWithRoles = _serviceRoles.GetAllUsers();

            if (usersWithRoles.Any())
            {
                // Выводим пользователей и их роли
                foreach (var userRoles in usersWithRoles)
                {
                    Console.WriteLine($"Пользователь: {userRoles.User.Name} (ID: {userRoles.User.Id})");
                    foreach (var role in userRoles.Roles)
                    {
                        Console.WriteLine($"  Роль: {role.Name} ({role.Type})");
                    }
                }
            }
            else
            {
                Console.WriteLine("Нет пользователей с ролями.");
            }
        }
    }
}
