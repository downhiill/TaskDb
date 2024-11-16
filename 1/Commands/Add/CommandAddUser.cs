using System;
using _1.Models.Entities;
using _1.Models.Interface;
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
        /// Инициализирует новый экземпляр <see cref="CommandAddUser"/>.
        /// </summary>
        /// <param name="service">Сервис, отвечающий за добавление пользователей.</param>
        public CommandAddUser(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Добавить пользователя";

        /// <summary>
        /// Выполняет команду добавления нового пользователя.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();

            Console.Write("Введите фамилию пользователя: ");
            string secondName = Console.ReadLine();

            Console.Write("Введите возраст пользователя: ");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите дату рождения:");
            DateTime dateOfbirth = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Введите З/п:");
            decimal wages = decimal.Parse(Console.ReadLine());

            // Создаем нового пользователя
            var user = new User { Name = name, SecondName = secondName, Age = age, Wages = wages, DateOfBirth = dateOfbirth };
            _service.Add(user); // Добавляем пользователя через сервис

            Console.WriteLine("Пользователь добавлен.");
        }
    }
}
