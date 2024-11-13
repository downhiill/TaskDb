using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _1.Commands.ShowAll
{
    public class CommandGetAllUsers : ICommand
    {
        private readonly ServiceRoles _serviceRoles;

        public CommandGetAllUsers(ServiceRoles serviceRoles)
        {
            _serviceRoles = serviceRoles;
        }

        public string Name => "Получить пользователей у которых есть хотя бы одна роль";
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
