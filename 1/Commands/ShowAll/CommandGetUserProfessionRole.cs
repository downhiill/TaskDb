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
    public class CommandGetUserProfessionRole : ICommand
    {
        private readonly ServiceProfession _serviceProfession;

        public CommandGetUserProfessionRole(ServiceProfession serviceProfession)
        {
            _serviceProfession = serviceProfession;
        }

        public string Name => "Получение информации о пользователях, их ролях и профессиях";

        public void Execute()
        {
            // Запрос названия профессии у пользователя
            Console.WriteLine("Введите название профессии:");
            string professionName = Console.ReadLine();

            // Запрос роли у пользователя
            Console.WriteLine("Введите роль (Admin, User, Guest):");
            if (!Enum.TryParse<EnumTypeRoles>(Console.ReadLine(), true, out EnumTypeRoles role))
            {
                Console.WriteLine("Неверная роль. Попробуйте снова.");
                return;
            }

            // Получение информации из сервиса
            var users = _serviceProfession.GetUserProfessionRole(professionName, role);

            // Вывод полученной информации
            if (users.Any())
            {
                Console.WriteLine($"Пользователи с профессией '{professionName}' и ролью '{role}':");
                foreach (var user in users)
                {
                    Console.WriteLine($"- ID: {user.User.Id}, Имя: {user.User.Name}, Дата рождения: {user.User.DateOfBirth?.ToString("yyyy-MM-dd") ?? "Не указана"}");
                    Console.WriteLine($"  Роль: {user.Role.Name}, Профессия: {user.ProfessionName}");
                }
            }
            else
            {
                Console.WriteLine($"Нет пользователей с профессией '{professionName}' и ролью '{role}'.");
            }
        }
    }
    
}
