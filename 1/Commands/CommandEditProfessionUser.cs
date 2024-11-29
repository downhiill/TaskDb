using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands
{
    public class CommandEditProfessionUser : ICommand
    {
        private readonly IServiceUsers _service;

        public CommandEditProfessionUser(IServiceUsers service)
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
