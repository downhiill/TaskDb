using _1.Models.Entities;
using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _1.Commands.ShowAll
{
    public class CommandGetUsers : ICommand
    {
        private readonly ServiceRoles _serviceRoles;

        public CommandGetUsers(ServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles;
        }

        public string Name => "Получаем список пользователей определенной роли";

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
