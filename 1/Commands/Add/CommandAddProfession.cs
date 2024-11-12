using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _1.Models.Interface;
using static _1.Services.ServiceUser;
using _1.Models.Entities;

namespace _1.Commands.Add
{
    public class CommandAddProfession : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandAddProfession(ServiceUsers service)
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
