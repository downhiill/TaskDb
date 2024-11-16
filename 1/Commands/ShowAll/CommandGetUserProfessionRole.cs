using _1.Models.Entities;
using _1.Models.Interface;
using _1.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1.Commands.ShowAll
{
    /// <summary>
    /// Команда для получения информации о пользователях, их ролях и профессиях.
    /// </summary>
    public class CommandGetUserProfessionRole : ICommand
    {
        private readonly ServiceProfession _serviceProfession;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandGetUserProfessionRole"/>.
        /// </summary>
        /// <param name="serviceProfession">Сервис для работы с профессиями пользователей.</param>
        public CommandGetUserProfessionRole(ServiceProfession serviceProfession)
        {
            _serviceProfession = serviceProfession ?? throw new ArgumentNullException(nameof(serviceProfession), "Сервис профессий не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Получение информации о пользователях, их ролях и профессиях";

        /// <summary>
        /// Выполняет команду, которая запрашивает профессию и роль у пользователя,
        /// а затем отображает информацию о пользователях с указанными параметрами.
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя название профессии и роль, затем получает список пользователей с заданными параметрами.
        /// Если такие пользователи найдены, они выводятся на экран. Если таких пользователей нет, выводится сообщение об этом.
        /// </remarks>
        public void Execute()
        {
            // Запрос названия профессии у пользователя
            Console.WriteLine("Введите название профессии:");
            string professionName = Console.ReadLine();

            // Запрос роли у пользователя
            Console.WriteLine("Введите роль (Admin, User, Guest):");
            if (!Enum.TryParse<EnumTypeRoles>(Console.ReadLine(), true, out EnumTypeRoles role))
            {
                Console.WriteLine("Неверная роль. Попробуйте снова.");
                return;
            }

            // Получение информации из сервиса
            var users = _serviceProfession.GetUserProfessionRole(professionName, role);

            // Вывод полученной информации
            if (users.Any())
            {
                Console.WriteLine($"Пользователи с профессией '{professionName}' и ролью '{role}':");
                foreach (var user in users)
                {
                    Console.WriteLine($"- ID: {user.User.Id}, Имя: {user.User.Name}, Дата рождения: {user.User.DateOfBirth?.ToString("yyyy-MM-dd") ?? "Не указана"}");
                    Console.WriteLine($"  Роль: {user.Role.Name}, Профессия: {user.ProfessionName}");
                }
            }
            else
            {
                Console.WriteLine($"Нет пользователей с профессией '{professionName}' и ролью '{role}'.");
            }
        }
    }
}
