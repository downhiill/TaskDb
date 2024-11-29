using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands
{
    public class CommandAddProfession : ICommand
    {
        private readonly IServiceUsers _service;

        public CommandAddProfession(IServiceUsers service)
        {
            _service = service;
        }

        public string Name => "Добавить профессию";

        public void Execute()
        {
            Console.Write("Введите название профессии: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название профессии не может быть пустым.");
                return;
            }

            // Передаем строку с названием профессии в метод AddProfession
            _service.AddProfession(name);

            Console.WriteLine("Профессия добавлена.");
        }
    }
}
