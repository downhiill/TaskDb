using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;

namespace _1.Commands.Edit
{
    internal class CommandEditAge
    {
        private readonly ServiceUsers _service;

        public CommandEditAge(ServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Изменить возраст";
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Введите новый возраст пользователя: ");
            int age = int.Parse(Console.ReadLine());

            _service.EditAge(userId, age);
            Console.WriteLine("Имя пользователя изменено.");
        }
    }
}
