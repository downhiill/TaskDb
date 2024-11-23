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
        var userDb = new UserDb
        {
            Name = user.Name,
            Age = user.Age
        };

        _context.Users.Add(userDb);
        _context.SaveChanges();
        Console.WriteLine($"User added: {userDb.Id}, {userDb.Name}, {userDb.Age}");

        // Проверка сохранения перед возвратом Id
        var savedUser = _context.Users.Find(userDb.Id);
        Console.WriteLine($"Saved user after save: {savedUser?.Name}");
        return userDb.Id;
    }

    /// <summary>
    /// Изменяет имя пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, чье имя необходимо изменить.</param>
    /// <param name="name">Новое имя пользователя.</param>
    public void EditName(int id, string name)
    {
        var userDb = _context.Users.FirstOrDefault(u => u.Id == id);
        if (userDb != null)
        {
            userDb.Name = name;
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Изменяет возраст пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, чей возраст необходимо изменить.</param>
    /// <param name="age">Новый возраст пользователя.</param>
    public void EditAge(int id, int age)
    {
        var userDb = _context.Users.FirstOrDefault(u => u.Id == id);
        if (userDb != null)
        {
            userDb.Age = age;
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Удаляет пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, которого нужно удалить.</param>
    public void Delete(int id)
    {
        var userDb = _context.Users.FirstOrDefault(u => u.Id == id);

        if (userDb != null)
        {
            _context.Users.Remove(userDb);
            _context.SaveChanges();
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
