using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;

namespace _1.Commands.Edit
{
    internal class CommandEditDateOfBirth
    {
        private readonly ServiceUsers _service;

        public CommandEditDateOfBirth(ServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Изменить дату рождения";
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Введите дату рождения: ");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            _service.EditDateOfBirth(userId, dateOfBirth);
            Console.WriteLine("Возраст пользователя изменен.");
        }
    }
}
