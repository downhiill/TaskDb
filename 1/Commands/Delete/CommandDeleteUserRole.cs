using _1.Models.Entities;
using _1.Models.Interface;
using _1.Services;

public class CommandDeleteUserRole : ICommand
{
    private readonly ServiceRoles _serviceRoles;

    public CommandDeleteUserRole(ServiceRoles serviceRoles)
    {
        _serviceRoles = serviceRoles;
    }

    public string Name => "Удаляем роль пользователю";

    public void Execute()
    {
        // Запрашиваем у пользователя данные для удаления роли
        Console.WriteLine("Введите ID пользователя:");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Некорректный ID пользователя.");
            return;
        }

        Console.WriteLine("Введите роль (например, Admin, User, Manager):");
        string roleName = Console.ReadLine();

        // Преобразуем строку в Enum
        if (Enum.TryParse(roleName, true, out EnumTypeRoles role))
        {
            // Удаляем роль у пользователя
            _serviceRoles.UserRemoveRole(userId, role);
            Console.WriteLine($"Роль {role} успешно удалена у пользователя с ID {userId}.");
        }
        else
        {
            Console.WriteLine("Некорректное имя роли.");
        }
    }
}
