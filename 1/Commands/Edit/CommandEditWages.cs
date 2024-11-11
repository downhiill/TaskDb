using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;

namespace _1.Commands.Edit
{
    internal class CommandEditWages
    {
        private readonly ServiceUsers _service;

        public CommandEditWages(ServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Изменить З/п";
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Введите новую заработную плату: ");
            decimal wages = decimal.Parse(Console.ReadLine());

            _service.EditWages(userId, wages);
            Console.WriteLine("Зарплата пользователя изменена.");
        }
    }
}
