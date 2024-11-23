using Project.Data;
using Project.IService;

public class ServiceUser : IServiceUsers
{
    private readonly ApplicationContext _context;

    // Конструктор принимает ApplicationContext через DI
    public ServiceUser(ApplicationContext context)
    {
        _context = context;
    }

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



    public void EditName(int id, string name)
    {
        var userDb = _context.Users.FirstOrDefault(u => u.Id == id);
        if (userDb != null)
        {
            userDb.Name = name;
            _context.SaveChanges();
        }
    }

    public void EditAge(int id, int age)
    {
        var userDb = _context.Users.FirstOrDefault(u => u.Id == id);
        if (userDb != null)
        {
            userDb.Age = age;
            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var userDb = _context.Users.FirstOrDefault(u => u.Id == id);

        if (userDb != null)
        {
            _context.Users.Remove(userDb);
            _context.SaveChanges();
        }
    }

    public List<UserModel> GetAllUsers()
    {
        return _context.Users
            .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
            .ToList();
    }

    public List<UserModel> SearchUsersMoreAge(int age)
    {
        return _context.Users
            .Where(u => u.Age > age)
            .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
            .ToList();
    }

    public List<UserModel> SearchUsers(string searchTerm)
    {
        return _context.Users
            .Where(u => u.Name.Contains(searchTerm))
            .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
            .ToList();
    }
}
