using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands.Edit
{
    public class CommandUpdateAccount : ICommand
    {
        private readonly ServiceAccount _serviceAccount;

        public CommandUpdateAccount(ServiceAccount serviceAccount)
        {
            _serviceAccount = serviceAccount;
        }

        public string Name => "Изменить данные аккаунта";

        public void Execute()
        {
            Console.WriteLine("Введите ID пользователя");
            int userId = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите новый логин");
            string login = Console.ReadLine();
            Console.WriteLine("Введите новый пароль:");
            string password = Console.ReadLine();

            _serviceAccount.UpdateAccount(userId, login, password);
            Console.WriteLine("Данные аккаунта успешно изменены");
        }
    }
}
