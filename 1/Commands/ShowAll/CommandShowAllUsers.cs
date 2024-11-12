using _1.Models.Interface;
using static _1.Services.ServiceUser;

namespace _1.Commands.ShowAll
{
    public class CommandShowAllUsers : ICommand
    {
        private readonly ServiceUsers _service;

        public CommandShowAllUsers(ServiceUsers service)
        {
            _service = service;
        }

        public string Name => "Вывод всех пользователей";

        // Метод Execute без параметров, чтобы соответствовать интерфейсу ICommand
        public void Execute()
        {
            ShowPagedUsers(0, 10); // Здесь передаются параметры пагинации
        }

        // Логика с пагинацией в отдельном методе
        public void ShowPagedUsers(int skip, int take)
        {
            var users = _service.GetAllUsers();
            var selectedUsers = users.Skip(skip).Take(take).ToList();

            if (selectedUsers.Count > 0)
            {
                foreach (var user in selectedUsers)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Зарплата: {user.Wages}, Возраст: {user.Age}");
                }
            }
            else
            {
                Console.WriteLine("Пользователи не найдены.");
            }
        }
    }
}
