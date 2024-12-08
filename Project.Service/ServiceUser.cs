using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.IService;

public class ServiceUser : IServiceUsers
{
    private readonly ApplicationContext _context;

    /// <summary>
    /// Конструктор для инъекции зависимости ApplicationContext.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public ServiceUser(ApplicationContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Добавляет нового пользователя в базу данных.
    /// </summary>
    /// <param name="user">Модель пользователя, который будет добавлен.</param>
    /// <returns>Идентификатор добавленного пользователя.</returns>
    public int Add(UserModel user)
    {
        // Проверяем, что имя не пустое
        if (string.IsNullOrWhiteSpace(user.Name))
        {
            throw new ArgumentException("User name cannot be empty or whitespace.");
        }

        // Проверяем, что пользователь с таким именем уже существует
        if (_context.Users.Any(u => u.Name == user.Name))
        {
            throw new InvalidOperationException($"A user with the name '{user.Name}' already exists.");
        }

        var userDb = new UserDb
        {
            Name = user.Name,
            Age = user.Age
        };

        _context.Users.Add(userDb);
        _context.SaveChanges();

        return userDb.Id;
    }

    /// <summary>
    /// Изменяет имя пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, чье имя необходимо изменить.</param>
    /// <param name="name">Новое имя пользователя.</param>
    public void EditName(int id, string name)
    {
        _context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdate(update => update.SetProperty(u => u.Name, name));
    }

    /// <summary>
    /// Изменяет возраст пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, чей возраст необходимо изменить.</param>
    /// <param name="age">Новый возраст пользователя.</param>
    public void EditAge(int id, int age)
    {
        _context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdate(update => update.SetProperty(u => u.Age, age));
    }

    /// <summary>
    /// Редактирует заработную плату пользователя по его идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чью зарплату нужно изменить.</param>
    /// <param name="wages">Новая заработная плата пользователя.</param>
    public void EditWages(int userId, decimal wages)
    {
        int affectedRows = _context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdate(update => update.SetProperty(u => u.Wages, wages));

        if (affectedRows == 0)
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }

    /// <summary>
    /// Редактирует дату рождения пользователя по его идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чью дату рождения нужно изменить.</param>
    /// <param name="dateOfBirth">Новая дата рождения пользователя.</param>
    public void EditDateOfBirth(int userId, DateTime dateOfBirth)
    {
        int affectedRows = _context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdate(update => update.SetProperty(u => u.DateOfBirth, dateOfBirth));

        if (affectedRows == 0)
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }

    /// <summary>
    /// Удаляет пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, которого нужно удалить.</param>
    public void Delete(int id)
    {
        int affectedRows = _context.Users
            .Where(u => u.Id == id)
            .ExecuteDelete();

        if (affectedRows == 0)
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }

    /// <summary>
    /// Получает всех пользователей из базы данных.
    /// </summary>
    /// <returns>Список всех пользователей.</returns>
    public List<UserModel> GetAllUsers()
    {
        return _context.Users
            .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
            .ToList();
    }

    /// <summary>
    /// Получает список пользователей с пагинацией и преобразует их в краткую информацию (ShortUser).
    /// </summary>
    /// <param name="skip">Количество пользователей, которых нужно пропустить.</param>
    /// <param name="take">Количество пользователей, которых нужно взять.</param>
    /// <returns>Список краткой информации о пользователях (ShortUser).</returns>
    public List<ShortUser> GetAllShortUsers(int skip, int take)
    {
        if (skip < 0 || take <= 0)
            throw new ArgumentException("Skip must be non-negative, and take must be greater than zero.");

        return _context.Users
            .Skip(skip)
            .Take(take)
            .Select(u => new ShortUser  // Преобразуем User в ShortUser
            {
                Id = u.Id,
                Name = u.Name,
                DateOfBirth = u.DateOfBirth
            })
            .ToList();
    }

    /// <summary>
    /// Ищет пользователей старше указанного возраста.
    /// </summary>
    /// <param name="age">Возраст, с которого начинается поиск.</param>
    /// <returns>Список пользователей старше указанного возраста.</returns>
    public List<UserModel> SearchUsersMoreAge(int age)
    {
        return _context.Users
            .Where(u => u.Age > age)
            .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
            .ToList();
    }

    /// <summary>
    /// Ищет пользователей по имени, совпадающему с заданным поисковым запросом.
    /// </summary>
    /// <param name="searchTerm">Поисковый запрос для имени пользователя.</param>
    /// <returns>Список пользователей, чьи имена содержат поисковый запрос.</returns>
    public List<UserModel> SearchUsers(string searchTerm)
    {
        return _context.Users
            .Where(u => u.Name.Contains(searchTerm))
            .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
            .ToList();
    }
}
