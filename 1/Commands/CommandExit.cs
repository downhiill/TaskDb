using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _1.Commands
{
    public class CommandExit : ICommand
    {
        private readonly IServiceUsers _service;

        public CommandExit(IServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Выход";  // Имя команды

        public void Execute()
        {
            Console.WriteLine("Выход из программы...");
            Environment.Exit(0);  // Завершаем выполнение программы
        }
    }
}
