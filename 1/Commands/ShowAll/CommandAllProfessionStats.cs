using System;
using System.Collections.Generic;
using _1.Models.Entities;
using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.ShowAll
{
    /// <summary>
    /// Команда для отображения статистики по профессиям и количеству сотрудников в каждой профессии.
    /// </summary>
    public class CommandAllProfessionStats : ICommand
    {
        private readonly ServiceUsers _service;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandAllProfessionStats"/>.
        /// </summary>
        /// <param name="service">Сервис для работы с пользователями.</param>
        public CommandAllProfessionStats(ServiceUsers service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Сервис пользователей не может быть null.");
        }

        /// <summary>
        /// Имя команды, которое будет отображаться в меню.
        /// </summary>
        public string Name => "Список всех профессий и кол-во сотрудников этой профессии";

        /// <summary>
        /// Выполняет команду, отображающую статистику по профессиям и количеству сотрудников в каждой профессии.
        /// </summary>
        /// <remarks>
        /// Запрашивает статистику по профессиям, выводит информацию о количестве сотрудников в каждой профессии.
        /// Если профессий нет, выводится сообщение об этом.
        /// </remarks>
        public void Execute()
        {
            // Получаем статистику по профессиям
            List<ModelProfessionStats> stats = _service.GetAllProfessionsStats();

            if (stats.Count == 0)
            {
                Console.WriteLine("Нет профессий в системе.");
                return;
            }

            // Выводим статистику на экран
            Console.WriteLine("Статистика по профессиям:");
            foreach (var stat in stats)
            {
                Console.WriteLine($"Профессия: {stat.Name}, Количество пользователей: {stat.Count}");
            }
        }
    }
}
