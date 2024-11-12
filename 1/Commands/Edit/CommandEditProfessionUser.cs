using _1.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;

namespace _1.Commands.Edit
{
    public class CommandEditProfessionUser : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandEditProfessionUser(ServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Изменить профессию";
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Введите новую профессию пользователя: ");
            int professionId = int.Parse(Console.ReadLine());

            _service.EditProfessionUser(userId, professionId);
            Console.WriteLine("Профессия пользователя изменена.");
        }
    }
}
