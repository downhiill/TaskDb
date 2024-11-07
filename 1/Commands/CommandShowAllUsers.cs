using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;
using System.Windows.Input;

namespace _1.Commands
{
    public class CommandShowAllUsers : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandShowAllUsers(ServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Вывод всех пользователей ";
        public void Execute()
        {
            var users = _service.GetAllUsers();

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
