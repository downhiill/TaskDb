using _1.Models.Entities;
using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands.Add
{
    public class CommandAddUserRole : ICommand
    {
        private readonly ServiceRoles _serviceRoles;

        public CommandAddUserRole(ServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles;
        }

        public string Name => "Добавляем роль пользователю";

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
