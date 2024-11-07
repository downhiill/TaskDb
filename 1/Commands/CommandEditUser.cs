using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;
using System.Windows.Input;

namespace _1.Commands
{
    public class CommandEditUser : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandEditUser(ServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Изменить данные";
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое имя пользователя: ");
            string name = Console.ReadLine();

            _service.EditName(userId, name);
            Console.WriteLine("Имя пользователя изменено.");
        }
    }
}
