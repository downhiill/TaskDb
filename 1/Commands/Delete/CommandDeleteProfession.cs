using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.Delete
{
    internal class CommandDeleteProfession : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandDeleteProfession(ServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Удалить профессию";
        public void Execute()
        {
            Console.Write("Введите ID профессии для удаления: ");
            int professionId = int.Parse(Console.ReadLine());

            _service.DeleteProfession(professionId);
            Console.WriteLine("Пользователь удален.");
        }
    }
}
