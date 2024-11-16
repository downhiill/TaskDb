using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.Models.Context;
using _1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _1.Services
{
    public class ServiceUser
    {
        public class ServiceUsers
        {
            private readonly ApplicationContext _db;

            public ServiceUsers(ApplicationContext dbContext)
            {
                _db = dbContext;
            }

            public void Add(User user)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            public void AddProfession(string name)
            {
                var profession = new Profession
                {
                    Name = name
                };

                _db.Professions.Add(profession);
                _db.SaveChanges();
            }

            public void EditName(int userId, string name)
            {
                var user = _db.Users.Find(userId);
                if (user != null)
                {
                    user.Name = name;
                    _db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Пользователь не найден.");
                }
            }

            public void EditAge(int userId, int age)
            {
                var user = _db.Users.Find(userId);
                if (user != null)
                {
                    user.Age = age;
                    _db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Пользователь не найден.");
                }
            }
            public void EditWages(int userId, decimal wages)
            {
                var user = _db.Users.Find(userId);
                if (user != null)
                {
                    user.Wages = wages;
                    _db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Пользователь не найден.");
                }
            }
            public void EditDateOfBirth(int userId, DateTime dateOfBirth)
            {
                var user = _db.Users.Find(userId);
                if (user != null)
                {
                    user.DateOfBirth = dateOfBirth;
                    _db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Пользователь не найден.");
                }
            }
            public void EditProfessionUser(int userId, int? professionId)
            {
                var user = _db.Users.Find(userId);
                if (user != null)
                {
                    user.ProfessionId = professionId;
                    _db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Профессия не найдена не найден.");
                }
            }

            public void Delete(int userId)
            {
                var user = _db.Users.Find(userId);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Пользователь не найден.");
                }
            }

            public void DeleteProfession(int professionId)
            {
                var profession = _db.Professions.Find(professionId);
                if(profession != null)
                {
                    _db.Professions.Remove(profession);
                    _db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Профессия не найдена.");
                }
            }

            public List<User> GetAllUsers()
            {
                return _db.Users.ToList();
            }

            // Метод для получения всех пользователей с пагинацией и преобразованием в ShortUser
            public List<ShortUser> GetAllShortUsers(int skip, int take)
            {
                return _db.Users
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
            public List<ModelUserProfession> GetAllProfessionsUsers()
            {
                return _db.Users
                    .Include(u => u.Profession)
                    .Select(u => new ModelUserProfession  
                    {
                        UserName = u.Name,
                        ProfessionName = u.Profession.Name
                    })
                    .ToList();
            }
            public List<ModelProfessionStats> GetAllProfessionsStats()
            {
                return _db.Professions
                    .Select(p => new ModelProfessionStats
                    {
                        Name = p.Name,
                        Count = p.Users.Count
                    })
                    .ToList();
            }

            public List<User> SearchUsersMoreAge(int age)
            {
                return _db.Users.Where(u => u.Age > age).ToList();
            }

            public List<User> SearchUsers(string term)
            {
                return _db.Users.Where(u => u.Name.Contains(term)).ToList();
            }
        }

    }
}