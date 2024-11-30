using Project.Service;
using System;

namespace _1.Commands
{
    /// <summary>
    /// Команда для обновления данных аккаунта пользователя.
    /// </summary>
    public class CommandUpdateAccount : ICommand
    {
        private readonly ServiceAccount _serviceAccount;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandUpdateAccount"/>.
        /// </summary>
        /// <param name="serviceAccount">Сервис для управления аккаунтами.</param>
        public CommandUpdateAccount(ServiceAccount serviceAccount)
        {
            _serviceAccount = serviceAccount;
        }

        /// <summary>
        /// Имя команды.
        /// </summary>
        public string Name => "Изменить данные аккаунта";

        /// <summary>
        /// Выполняет команду для изменения данных аккаунта пользователя.
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("Введите ID пользователя");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Введите новый логин");
                string login = Console.ReadLine();
                Console.WriteLine("Введите новый пароль:");
                string password = Console.ReadLine();

                try
                {
                    // Обновление данных аккаунта
                    _serviceAccount.UpdateAccount(userId, login, password);
                    Console.WriteLine("Данные аккаунта успешно изменены");
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
