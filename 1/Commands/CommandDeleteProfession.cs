using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands
{
    public class CommandDeleteProfession : ICommand
    {
        private readonly IServiceUsers _service;

        public CommandDeleteProfession(IServiceUsers service)
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
