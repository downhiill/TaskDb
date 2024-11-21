using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _1.Commands
{
    public class CommandDeleteUser : ICommand
    {
        private readonly IServiceUsers _service;

        public CommandDeleteUser(IServiceUsers service)
        {
            _service = service;
        }
        public string Name => "Удалить пользователя";
        public void Execute()
        {
            Console.Write("Введите ID пользователя для удаления: ");
            int userId = int.Parse(Console.ReadLine());

            _service.Delete(userId);
            Console.WriteLine("Пользователь удален.");
        }
    }
}
