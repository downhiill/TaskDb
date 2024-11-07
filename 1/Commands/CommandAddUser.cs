using _1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;
using System.Windows.Input;

namespace _1.Commands
{
    public class CommandAddUser : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandAddUser(ServiceUsers service)
        {
            _service = service;
        }

        public string Name => "Добавить пользователя";

        public void Execute()
        {
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();
            Console.Write("Введите возраст пользователя: ");
            int age = int.Parse(Console.ReadLine());

            var user = new User { Name = name, Age = age };
            _service.Add(user);

            Console.WriteLine("Пользователь добавлен.");
        }
    }
}
