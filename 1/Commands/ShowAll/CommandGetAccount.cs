using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands.ShowAll
{
    public class CommandGetAccount : ICommand
    {
        private readonly ServiceAccount _serviceAccount;

        public CommandGetAccount(ServiceAccount serviceAccount)
        {
            _serviceAccount = serviceAccount;
        }

        public string Name => "Вывести информацию об аккаунте";

        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                try
                {
                    var account = _serviceAccount.GetAccount(userId);
                    if (account != null)
                    {
                        Console.WriteLine($"Логин: {account.Login}");
                        Console.WriteLine($"Пароль: {account.Password}");
                    }
                    else
                    {
                        Console.WriteLine("Аккаунт не найден для указанного ID пользователя.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Убедитесь, что ID пользователя является числом.");
            }
        }
    }
}
