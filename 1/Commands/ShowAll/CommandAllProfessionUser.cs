using _1.Models.Entities;
using _1.Models.Interface;
using System;
using System.Collections.Generic;
using static _1.Services.ServiceUser;

namespace _1.Commands.ShowAll
{
    /// <summary>
    /// Команда для отображения списка пользователей и их профессий.
    /// </summary>
    public class CommandAllProfessionUser : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandAllProfessionUser"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandAllProfessionUser(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Список пользователей и их профессии";

        /// <summary>
        /// Выполняет команду, отображающую список пользователей и их профессий.
        /// </summary>
        /// <remarks>
        /// Запрашивает список пользователей с их профессиями, выводит информацию о пользователях и профессиях.
        /// Если пользователей нет, выводится сообщение об этом.
        /// </remarks>
        public void Execute()
        {
            // Получаем список пользователей с их профессиями
            List<ModelUserProfession> userProfessionList = _service.GetAllProfessionsUsers();

            if (userProfessionList.Count == 0)
            {
                Console.WriteLine("Нет пользователей в системе.");
                return;
            }

            // Выводим список пользователей и их профессий
            Console.WriteLine("Список пользователей и их профессий:");
            foreach (var userProfession in userProfessionList)
            {
                Console.WriteLine($"Пользователь: {userProfession.UserName}, Профессия: {userProfession.ProfessionName}");
            }
        }
    }
}
