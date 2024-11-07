using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;

namespace _1.Commands
{
    public class CommandSearchUserAge : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandSearchUserAge(ServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Поиск по возрасту";
        public void Execute()
        {
            // Запрос возраста для поиска
            Console.Write("Введите минимальный возраст для поиска пользователей: ");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.WriteLine("Пожалуйста, введите корректное число.");
            }

            // Используем метод для поиска пользователей старше указанного возраста
            var users = _service.SearchUsersMoreAge(age);

            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Возраст: {user.Age}");
                }
            }
            else
            {
                Console.WriteLine("Пользователи не найдены.");
            }
        }
    }
}
