using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;
using _1.Models.Interface;

namespace _1.Commands.Edit
{
    public class CommandEditName : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandEditName(ServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Изменить имя";
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
