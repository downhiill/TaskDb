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
    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    public class ServiceUser
    {
        /// <summary>
        /// Класс, содержащий методы для работы с пользователями.
        /// </summary>
        public class ServiceUsers
        {
            private readonly ApplicationContext _db;

            /// <summary>
            /// Инициализирует сервис с контекстом базы данных.
            /// </summary>
            /// <param name="dbContext">Контекст базы данных.</param>
            public ServiceUsers(ApplicationContext dbContext)
            {
                _db = dbContext;
            }

            /// <summary>
            /// Добавляет нового пользователя в базу данных.
            /// </summary>
            /// <param name="user">Пользователь для добавления.</param>
            public void Add(User user)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
            }

            /// <summary>
            /// Добавляет новую профессию в базу данных.
            /// </summary>
            /// <param name="name">Название профессии.</param>
            public void AddProfession(string name)
            {
                var profession = new Profession
                {
                    Name = name
                };

                _db.Professions.Add(profession);
                _db.SaveChanges();
            }

            /// <summary>
            /// Изменяет имя пользователя.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя.</param>
            /// <param name="name">Новое имя.</param>
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
            /// Изменяет возраст пользователя.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя.</param>
            /// <param name="age">Новый возраст.</param>
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
            /// Изменяет заработную плату пользователя.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя.</param>
            /// <param name="wages">Новая заработная плата.</param>
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
            /// Изменяет дату рождения пользователя.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя.</param>
            /// <param name="dateOfBirth">Новая дата рождения.</param>
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
            /// Изменяет профессию пользователя.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя.</param>
            /// <param name="professionId">Идентификатор новой профессии.</param>
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
                    Console.WriteLine("Профессия не найдена.");
                }
            }

            /// <summary>
            /// Удаляет пользователя из базы данных.
            /// </summary>
            /// <param name="userId">Идентификатор пользователя для удаления.</param>
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
            /// Удаляет профессию из базы данных.
            /// </summary>
            /// <param name="professionId">Идентификатор профессии для удаления.</param>
            public void DeleteProfession(int professionId)
            {
                var profession = _db.Professions.Find(professionId);
                if (profession != null)
                {
                    _db.Professions.Remove(profession);
                    _db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Профессия не найдена.");
                }
            }

            /// <summary>
            /// Получает список всех пользователей.
            /// </summary>
            /// <returns>Список всех пользователей.</returns>
            public List<User> GetAllUsers()
            {
                return _db.Users.ToList();
            }

            /// <summary>
            /// Получает пользователей с пагинацией и преобразует их в объекты типа ShortUser.
            /// </summary>
            /// <param name="skip">Количество пользователей, которые следует пропустить.</param>
            /// <param name="take">Количество пользователей, которые следует выбрать.</param>
            /// <returns>Список пользователей с пагинацией в виде объектов типа ShortUser.</returns>
            public List<ShortUser> GetAllShortUsers(int skip, int take)
            {
                return _db.Users
                    .Skip(skip)
                    .Take(take)
                    .Select(u => new ShortUser
                    {
                        Id = u.Id,
                        Name = u.Name,
                        DateOfBirth = u.DateOfBirth
                    })
                    .ToList();
            }

            /// <summary>
            /// Получает список всех пользователей и их профессий.
            /// </summary>
            /// <returns>Список пользователей с их профессиями.</returns>
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

            /// <summary>
            /// Получает статистику по профессиям (количество пользователей в каждой профессии).
            /// </summary>
            /// <returns>Список профессий с количеством пользователей в каждой.</returns>
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

            /// <summary>
            /// Находит пользователей старше указанного возраста.
            /// </summary>
            /// <param name="age">Возраст для фильтрации.</param>
            /// <returns>Список пользователей старше указанного возраста.</returns>
            public List<User> SearchUsersMoreAge(int age)
            {
                return _db.Users.Where(u => u.Age > age).ToList();
            }

            /// <summary>
            /// Ищет пользователей по части имени.
            /// </summary>
            /// <param name="term">Термин для поиска в имени пользователя.</param>
            /// <returns>Список пользователей, чьи имена содержат указанный термин.</returns>
            public List<User> SearchUsers(string term)
            {
                return _db.Users.Where(u => u.Name.Contains(term)).ToList();
            }
        }
    }
}
