using System;
using _1.Models.Interface;
using _1.Models.Entities;
using static _1.Services.ServiceUser;

namespace _1.Commands.Add
{
    /// <summary>
    /// Команда для добавления новой профессии.
    /// </summary>
    public class CommandAddProfession : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandAddProfession"/>.
        /// </summary>
        /// <param name="service">Сервис, отвечающий за добавление профессий.</param>
        public CommandAddProfession(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Добавить профессию";

        /// <summary>
        /// Выполняет команду добавления профессии.
        /// </summary>
        public void Execute()
        {
            Console.Write("Введите название профессии: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название профессии не может быть пустым.");
                return;
            }

            // Передаем строку с названием профессии в метод AddProfession
            _service.AddProfession(name);

            Console.WriteLine("Профессия добавлена.");
        }
    }
}
