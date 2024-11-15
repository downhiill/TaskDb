using _1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _1.Services.ServiceUser;

namespace _1.Commands.DeletOfAdd
{
    /// <summary>
    /// Команда для добавления нового пользователя.
    /// </summary>
    public class CommandAddUser : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр команды для добавления пользователя.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandAddUser(ServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Имя команды.
        /// </summary>
        public string Name => "Добавить пользователя";

        /// <summary>
        /// Выполняет команду добавления нового пользователя.
        /// Запрашивает данные у пользователя и добавляет их в систему.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();
            Console.Write("Введите возраст пользователя: ");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите дату рождения:");
            DateTime dateOfbirth = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите З/п:");
            decimal wages = decimal.Parse(Console.ReadLine());

            var user = new User { Name = name, Age = age, Wages = wages, DateOfBirth = dateOfbirth };
            _service.Add(user);

            Console.WriteLine("Пользователь добавлен.");
        }
    }
}
