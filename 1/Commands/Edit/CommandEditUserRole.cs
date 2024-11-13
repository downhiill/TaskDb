using _1.Models.Entities;
using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands.Edit
{
    public class CommandEditUserRole : ICommand
    {
        private readonly ServiceRoles _serviceRoles;

        public CommandEditUserRole(ServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles;
        }

        public string Name => "Изменить роль у пользователя";

        public void Execute ()
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
