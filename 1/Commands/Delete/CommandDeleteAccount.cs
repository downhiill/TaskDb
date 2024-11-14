using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands.Delete
{
    public class CommandDeleteAccount : ICommand
    {
        private readonly ServiceAccount _serviceAccount;

        public CommandDeleteAccount(ServiceAccount serviceAccount)
        {
            _serviceAccount = serviceAccount;
        }

        public string Name => "Удаление аккаунта";

        public void Execute()
        {
            Console.WriteLine("Введите ID аккаунта:");
            int userId = int.Parse(Console.ReadLine());

            _serviceAccount.RemoveAccount(userId);
            Console.WriteLine("Аккаунт успешно удален");
        }
    }
}
