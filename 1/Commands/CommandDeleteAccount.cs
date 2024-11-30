using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands
{
    /// <summary>
    /// Команда для удаления аккаунта пользователя.
    /// </summary>
    public class CommandDeleteAccount : ICommand
    {
        private readonly ServiceAccount _serviceAccount;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandDeleteAccount"/>.
        /// </summary>
        /// <param name="serviceAccount">Сервис для управления аккаунтами.</param>
        public CommandDeleteAccount(ServiceAccount serviceAccount)
        {
            _serviceAccount = serviceAccount;
        }

        /// <summary>
        /// Имя команды.
        /// </summary>
        public string Name => "Удаление аккаунта";

        /// <summary>
        /// Выполняет команду удаления аккаунта.
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("Введите ID аккаунта:");
            int userId;
            if (int.TryParse(Console.ReadLine(), out userId))
            {
                try
                {
                    _serviceAccount.RemoveAccount(userId);
                    Console.WriteLine("Аккаунт успешно удален.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении аккаунта: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Введено некорректное значение для ID.");
            }
        }
    }
}
