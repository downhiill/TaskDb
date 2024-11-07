using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;
using System.Windows.Input;

namespace _1.Commands
{
    public class CommandSearchUsers : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandSearchUsers(ServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Поиск по имени";
        public void Execute()
        {
            Console.Write("Введите часть имени пользователя для поиска: ");
            string term = Console.ReadLine();

            var users = _service.SearchUsers(term);

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
