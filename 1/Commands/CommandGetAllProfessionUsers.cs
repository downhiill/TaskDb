using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Commands
{
    public class CommandAllProfessionUser : ICommand
    {
        private readonly IServiceUsers _service;

        public CommandAllProfessionUser(IServiceUsers service)
        {
            _service = service;
        }

        public string Name => "Список пользователей и их профессии";

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
