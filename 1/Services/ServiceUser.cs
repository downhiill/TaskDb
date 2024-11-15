using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.Models;
using Microsoft.EntityFrameworkCore;

namespace _1.Services
{
    /// <summary>
    /// Сервис для работы с пользователями в базе данных.
    /// </summary>
    public class ServiceUser
    {
        /// <summary>
        /// Сервис пользователей для взаимодействия с базой данных.
        /// </summary>
        public class ServiceUsers
        {
            private readonly ApplicationContext _db;

            /// <summary>
            /// Инициализирует новый экземпляр сервиса пользователей.
            /// </summary>
            /// <param name="dbContext">Контекст базы данных, который будет использоваться для операций.</param>
            public ServiceUsers(ApplicationContext dbContext)
            {
                _db = dbContext;
            }

            /// <summary>
            /// Добавляет нового пользователя в базу данных.
            /// </summary>
            /// <param name="user">Пользователь, который будет добавлен.</param>
            public void Add(User user)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
            }

            /// <summary>
            /// Редактирует имя пользователя по его идентификатору.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя, чье имя нужно изменить.</param>
            /// <param name="name">Новое имя пользователя.</param>
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

            /// <summary>
            /// Редактирует возраст пользователя по его идентификатору.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя, чей возраст нужно изменить.</param>
            /// <param name="age">Новый возраст пользователя.</param>
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

            /// <summary>
            /// Редактирует заработную плату пользователя по его идентификатору.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя, чью зарплату нужно изменить.</param>
            /// <param name="wages">Новая заработная плата пользователя.</param>
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

            /// <summary>
            /// Редактирует дату рождения пользователя по его идентификатору.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя, чью дату рождения нужно изменить.</param>
            /// <param name="dateOfBirth">Новая дата рождения пользователя.</param>
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

            /// <summary>
            /// Удаляет пользователя по его идентификатору.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя, которого нужно удалить.</param>
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

            /// <summary>
            /// Получает всех пользователей из базы данных.
            /// </summary>
            /// <returns>Список всех пользователей.</returns>
            public List<User> GetAllUsers()
            {
                return _db.Users.ToList();
            }

            /// <summary>
            /// Получает список пользователей с пагинацией и преобразует их в краткую информацию (ShortUser).
            /// </summary>
            /// <param name="skip">Количество пользователей, которых нужно пропустить.</param>
            /// <param name="take">Количество пользователей, которых нужно взять.</param>
            /// <returns>Список краткой информации о пользователях (ShortUser).</returns>
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

            /// <summary>
            /// Ищет пользователей старше указанного возраста.
            /// </summary>
            /// <param name="age">Минимальный возраст для поиска пользователей.</param>
            /// <returns>Список пользователей, чей возраст больше указанного.</returns>
            public List<User> SearchUsersMoreAge(int age)
            {
                return _db.Users.Where(u => u.Age > age).ToList();
            }

            /// <summary>
            /// Ищет пользователей по части имени.
            /// </summary>
            /// <param name="term">Часть имени пользователя для поиска.</param>
            /// <returns>Список пользователей, чье имя содержит указанную часть.</returns>
            public List<User> SearchUsers(string term)
            {
                return _db.Users.Where(u => u.Name.Contains(term)).ToList();
            }
        }
    }
}
