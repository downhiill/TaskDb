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

    public void Add(UserModel user)
    {
        var userDb = new UserDb
        {
            Name = user.Name,
            Age = user.Age
        };

        _context.Users.Add(userDb);
        _context.SaveChanges();
    }

    public void EditName(int id, string name)
    {
        var userDb = _context.Users.Find(id);
        if (userDb != null)
        {
            userDb.Name = name;
            _context.SaveChanges();
        }
    }

    public void EditAge(int id, int age)
    {
        var userDb = _context.Users.Find(id);
        if (userDb != null)
        {
            userDb.Age = age;
            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var userDb = _context.Users.Find(id);
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
