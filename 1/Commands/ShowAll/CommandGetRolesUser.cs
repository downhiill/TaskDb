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
    public class CommandGetRolesUser : ICommand
    {
        private readonly ServiceRoles _serviceRoles;

        public CommandGetRolesUser(ServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles;
        }

        public string Name => "Получаем список всех ролей заданного пользователя";

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
