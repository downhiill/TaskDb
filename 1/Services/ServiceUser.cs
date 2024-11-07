using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.Models;
using Microsoft.EntityFrameworkCore;

namespace _1.Services
{
    public class ServiceUser
    {
        public class ServiceUsers
        {
            private readonly ApplicationContext _db;

            public ServiceUsers()
            {
                _db = new ApplicationContext();
            }

            public void Add(User user)
            {
                _db.Users.Add(user);
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

            public List<User> GetAllUsers()
            {
                return _db.Users.ToList();
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
