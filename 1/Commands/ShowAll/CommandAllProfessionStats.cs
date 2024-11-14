
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.Models.Entities.Short;
using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.ShowAll
{
    public class CommandAllProfessionStats : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandAllProfessionStats(ServiceUsers service)
        {
            _service = service;
        }

        public string Name => "Список всех профессий и кол-во сотрудников этой профессии";

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
