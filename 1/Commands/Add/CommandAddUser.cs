using System;
using _1.Models.Entities;
using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.DeletOfAdd
{
    public class CommandAddUser : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandAddUser(ServiceUsers service)
        {
            _service = service;
        }

        public string Name => "Добавить пользователя";

        public void Execute()
        {
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();

            Console.Write("Введите фамилию пользователя: ");
            string secondName = Console.ReadLine();

            Console.Write("Введите возраст пользователя: ");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите дату рождения:");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Введите З/п:");
            decimal wages = decimal.Parse(Console.ReadLine());

            // Создание пользователя с учётом всех новых полей
            var user = new User
            {
                Name = name,
                SecondName = secondName,
                Age = age,
                DateOfBirth = dateOfBirth,
                Wages = wages,
                Active = true,  // По умолчанию новый пользователь активен
            };

            // Добавление пользователя через сервис
            _service.Add(user);

            Console.WriteLine("Пользователь добавлен.");
        }
    }
}
