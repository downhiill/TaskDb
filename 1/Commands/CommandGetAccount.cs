using Project.Service;
using System;

namespace _1.Commands
{
    /// <summary>
    /// Команда для получения информации об аккаунте пользователя.
    /// </summary>
    public class CommandGetAccount : ICommand
    {
        private readonly ServiceAccount _serviceAccount;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandGetAccount"/>.
        /// </summary>
        /// <param name="serviceAccount">Сервис для управления аккаунтами.</param>
        public CommandGetAccount(ServiceAccount serviceAccount)
        {
            _serviceAccount = serviceAccount;
        }

        /// <summary>
        /// Имя команды.
        /// </summary>
        public string Name => "Вывести информацию об аккаунте";

        /// <summary>
        /// Выполняет команду для получения информации об аккаунте пользователя.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите ID пользователя: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                try
                {
                    // Получаем информацию об аккаунте
                    var account = _serviceAccount.GetAccount(userId);
                    if (account != null)
                    {
                        // Выводим данные аккаунта
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
                    // Обрабатываем исключения
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                // Обрабатываем некорректный ввод
                Console.WriteLine("Некорректный ввод. Убедитесь, что ID пользователя является числом.");
            }
        }
    }
}
