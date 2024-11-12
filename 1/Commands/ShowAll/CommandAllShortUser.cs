using _1.Services;
using System;
using System.Collections.Generic;
using static _1.Services.ServiceUser;
using _1.Models.Entities;
using _1.Models.Interface;

namespace _1.Commands.ShowAll
{
    public class CommandShowShortUsers : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandShowShortUsers(ServiceUsers service)
        {
            _service = service;
        }

        public string Name => "Вывод краткой информации о пользователях";

        public void Execute()
        {
            int skip = 0;  // Пропускаем 0 пользователей
            int take = 10; // Берём 10 пользователей

            List<ShortUser> users = _service.GetAllShortUsers(skip, take);

            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Дата Рождения: {user.DateOfBirth}");
                }
            }
            else
            {
                Console.WriteLine("Пользователи не найдены.");
            }
        }
    }
}
