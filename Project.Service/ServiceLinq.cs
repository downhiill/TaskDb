using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class ServiceLinq : IServiceLinq
    {
        private readonly ApplicationContext _context;

        /// <summary>
        /// Конструктор для инъекции зависимости ApplicationContext.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public ServiceLinq(ApplicationContext context)
        {
            _context = context;
        }

        public List<UserModel> SearchUsersByNamePattern(string namePattern)
        {
            return _context.Users
                .Where(u => EF.Functions.Like(u.Name, namePattern))
                .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
                .ToList();
        }

        public void Delete(int id)
        {
            // Найти пользователя по ID
            var user = _context.Users.Find(id);

            if (user == null)
            {
                Console.WriteLine("Пользователь не найден.");
                return;
            }

            // Удалить найденного пользователя
            _context.Users.Remove(user);
            _context.SaveChanges();

            Console.WriteLine("Пользователь успешно удален.");
        }


        public void EditWages(int userId, decimal wages)
        {
            // Найти пользователя по ID
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                Console.WriteLine("Пользователь не найден.");
                return;
            }

            // Обновить значение Wages
            user.Wages = wages;

            // Сохранить изменения в базе данных
            _context.SaveChanges();

            Console.WriteLine("Заработная плата пользователя успешно обновлена.");
        }

        public List<UserModel> GetAllUsersOrderBy()
        {
            return _context.Users
                .OrderBy(u => u.Name)  
                .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
                .ToList();
        }

        public List<UserModel> GetAllUsersOrderByDescending()
        {
            return _context.Users
                .OrderByDescending(u => u.Name)  
                .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
                .ToList();
        }

        public List<UserModel> GetAllUsersThenBy()
        {
            return _context.Users
                .OrderBy(u => u.Age)       
                .ThenBy(u => u.Name)       
                .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
                .ToList();
        }

        public void UserChangeRole(int userId, List<EnumTypeRoleModel> roles)
        {
            // Удаление существующих ролей для пользователя
            var existingRoles = _context.RolesUsers.Where(ru => ru.UserId == userId).ToList();
            _context.RolesUsers.RemoveRange(existingRoles); // Удаляем все старые роли

            // Получаем новые роли через join с таблицей Roles
            var newRoles = (from role in roles
                            join r in _context.Roles on (EnumTypeRoleDb)role equals r.Id // Соединяем с таблицей ролей
                            select new RolesUsers
                            {
                                UserId = userId,
                                RoleId = r.Id
                            }).ToList();

            // Добавление новых ролей в таблицу RolesUsers
            _context.RolesUsers.AddRange(newRoles);
            _context.SaveChanges(); // Сохраняем изменения в базе данных
        }

        public List<ShortUserProfessionRole> GetUserProfessionRole(string nameProfession, EnumTypeRoleModel role)
        {
            EnumTypeRoleDb dbRole = (EnumTypeRoleDb)(int)role; // Преобразование EnumTypeRoleModel в EnumTypeRoleDb

            return (from u in _context.Users
                    join ru in _context.RolesUsers on u.Id equals ru.UserId // Соединяем Users и RolesUsers
                    join r in _context.Roles on ru.RoleId equals r.Id // Соединяем RolesUsers и Roles
                    join p in _context.Professions on u.ProfessionId equals p.Id // Соединяем Users с Professions
                    where p.Name == nameProfession && r.Id == dbRole // Фильтруем по профессии и роли
                    select new ShortUserProfessionRole
                    {
                        User = new ShortUser
                        {
                            Id = u.Id,
                            Name = u.Name,
                            DateOfBirth = u.DateOfBirth
                        },
                        Role = new ShortRole
                        {
                            Type = role,
                            Name = r.Name
                        },
                        ProfessionName = p.Name
                    }).ToList();
        }

        public List<IGrouping<int, UserModel>> GetAllUsersGroupedByAge()
        {
            return _context.Users
                .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
                .ToList() // Переводим в список, чтобы избежать ошибки при группировке
                .GroupBy(u => u.Age)  // Группировка пользователей по возрасту
                .ToList();  // Переводим в список после группировки
        }

        public List<UserModel> GetActiveAndOldUsers()
        {
            var combinedUsers = _context.Users
                .Where(u => u.Active && u.Age > 30)  // Filter users that are both active and older than 30
                .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
                .ToList();

            return combinedUsers;
        }

        public List<UserModel> GetActiveAndOldUsersIntersect()
        {
            var activeUsers = _context.Users
                .Where(u => u.Active)
                .Select(u => new { u.Id, u.Name, u.Age })
                .ToList();

            var oldUsers = _context.Users
                .Where(u => u.Age > 30)
                .Select(u => new { u.Id, u.Name, u.Age })
                .ToList();

            // Используем Intersect для нахождения общих пользователей по Id, Name и Age
            var commonUsers = activeUsers
                .Intersect(oldUsers)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Age = u.Age
                })
                .ToList();

            return commonUsers;
        }

        public bool AnyActiveUserOver30()
        {
            bool anyActiveUserOver30 = _context.Users
                .Any(u => u.Active && u.Age > 30);

            return anyActiveUserOver30;
        }

        public bool AllUsersOver18()
        {
            bool allUsersOver18 = _context.Users
                .All(u => u.Age > 18);

            return allUsersOver18;
        }

        public int CountUsersOver18()
        {
            int count = _context.Users
                .Count(u => u.Age > 18);

            return count;
        }

        public int GetMinAge()
        {
            int minAge = _context.Users
                .Min(u => u.Age);

            return minAge;
        }

        public int GetMaxAge()
        {
            int maxAge = _context.Users
                .Max(u => u.Age);

            return maxAge;
        }

        public double GetAverageAge()
        {
            double averageAge = _context.Users
                .Average(u => u.Age);

            return averageAge;
        }

        public decimal GetTotalWages()
        {
            decimal totalWages = _context.Users
                .Sum(u => u.Wages);

            return totalWages;
        }

        public decimal GetTotalWagesNoTracking()
        {
            decimal totalWages = _context.Users
                .AsNoTracking()  // Отключаем отслеживание изменений
                .Sum(u => u.Wages);

            return totalWages;
        }

        public decimal GetTotalWagesNoTrackingUsingTrackingBehavior()
        {
            // Устанавливаем поведение отслеживания для всех запросов в контексте
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;

            decimal totalWages = _context.Users
                .Sum(u => u.Wages);

            // Опционально: восстановить поведение отслеживания обратно, если нужно
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll;

            return totalWages;
        }

        public void TrackChangesForEntity(int userId)
        {
            var user = _context.Users.Find(userId);

            // Изменяем данные
            user.Name = "New Name";

            // Проверяем изменения
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Modified)
            {
                Console.WriteLine($"User with ID {userId} was modified.");
            }

            _context.SaveChanges();
        }

        public IEnumerable<UserModel> GetAllUsersIEnumerable()
        {
            return _context.Users
                .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age })
                .AsEnumerable(); // Преобразуем в IEnumerable
        }

        public IQueryable<UserModel> GetAllUsersIQueryable()
        {
            return _context.Users
                .Select(u => new UserModel { Id = u.Id, Name = u.Name, Age = u.Age });
        }



    }
}
