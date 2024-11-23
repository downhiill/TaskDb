using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _1.Commands
{
    /// <summary>
    /// Команда для добавления нового пользователя.
    /// Реализует интерфейс ICommand.
    /// </summary>
    public class CommandAddUser : ICommand
    {
        private readonly IServiceUsers _service;

        /// <summary>
        /// Конструктор, который инициализирует сервис пользователей.
        /// </summary>
        /// <param name="service">Интерфейс сервиса для работы с пользователями.</param>
        public CommandAddUser(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Название команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Добавить пользователя";

        /// <summary>
        /// Выполняет добавление нового пользователя.
        /// Запрашивает имя и возраст пользователя у пользователя.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();
            Console.Write("Введите возраст пользователя: ");
            int age = int.Parse(Console.ReadLine());

            var user = new UserModel { Name = name, Age = age };
            _service.Add(user);

            Console.WriteLine("Пользователь добавлен.");
        }
    }
}
