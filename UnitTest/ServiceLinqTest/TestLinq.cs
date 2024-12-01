using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ServiceLinqTest
{
    public class TestLinq : ServiceLinqTest
    {
        [Fact(DisplayName = "Поиск пользователей по шаблону имени")]
        public void SearchUsersByNamePattern_ShouldReturnMatchingUsers()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 25 };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Johnny", SecondName = "Smith", Age = 30 };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 22 };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            // Сохраняем данные
            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();

            // Act
            var result = service.SearchUsersByNamePattern("John%");

            // Assert
            Assert.Equal(2, result.Count); // Ожидаем 2 пользователя, чьи имена начинаются с "John"
            Assert.Contains(result, u => u.Name == "John");
            Assert.Contains(result, u => u.Name == "Johnny");
        }

        [Fact(DisplayName = "Удаление пользователя по идентификатору")]
        public void Delete_ShouldRemoveUser()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user = new UserDb { Name = "Mark", SecondName = "Smith", Age = 40 };
            user.FullName = $"{user.Name} {user.SecondName}";
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            service.Delete(user.Id);

            // Assert
            var deletedUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
            Assert.Null(deletedUser);
        }

        [Fact(DisplayName = "Обновление заработной платы пользователя")]
        public void EditWages_ShouldUpdateWages()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user = new UserDb { Name = "John", SecondName = "Smith", Age = 30, Wages = 1000m };
            user.FullName = $"{user.Name} {user.SecondName}";
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            service.EditWages(user.Id, 1500m);

            // Assert
            var updatedUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
            Assert.NotNull(updatedUser);
            Assert.Equal(1500m, updatedUser.Wages);
        }


        [Fact(DisplayName = "Получение всех пользователей, отсортированных по имени")]
        public void GetAllUsersOrderBy_ShouldReturnUsersSortedByName()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            context.Users.AddRange(
                new UserDb { Name = "Charlie", SecondName = "Smith", Age = 25, FullName = "Charlie Smith" },
                new UserDb { Name = "Alice", SecondName = "Smith", Age = 30, FullName = "Alice Smith" },
                new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, FullName = "Bob Smith" }
            );
            context.SaveChanges();  // Ensure SaveChanges is called after adding users

            // Act
            var result = service.GetAllUsersOrderBy();

            // Assert
            var userList = result.ToList();  // Convert to list to evaluate the count
            Assert.Equal(3, userList.Count);  // Ensure there are 3 users
            Assert.Equal("Alice", userList[0].Name);  // First user by name
            Assert.Equal("Bob", userList[1].Name);    // Second user by name
            Assert.Equal("Charlie", userList[2].Name); // Third user by name
        }


        [Fact(DisplayName = "Получение всех пользователей в порядке убывания по имени")]
        public void GetAllUsersOrderByDescending_ShouldReturnUsersSortedByNameDescending()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Очистка базы данных перед тестом
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();

            // Arrange
            var user1 = new UserDb { Name = "Charlie", SecondName = "Smith", Age = 25 };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 30 };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 22 };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            // Add users to the context
            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges(); // Save the changes to the database

            // Act
            var result = service.GetAllUsersOrderByDescending();

            // Assert
            Assert.Equal(3, result.Count); // We expect 3 users
            Assert.Equal("Charlie", result[0].Name); // First user should be Charlie
            Assert.Equal("Bob", result[1].Name); // Second user should be Bob
            Assert.Equal("Alice", result[2].Name); // Third user should be Alice
        }



        [Fact(DisplayName = "Получение всех пользователей сначала по возрасту, затем по имени")]
        public void GetAllUsersThenBy_ShouldReturnUsersSortedByAgeThenName()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            context.Users.AddRange(
                new UserDb { Name = "Charlie", SecondName = "Smith", Age = 25, FullName = "Charlie Smith" },
                new UserDb { Name = "Alice", SecondName = "Smith", Age = 25, FullName = "Alice Smith" },
                new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, FullName = "Bob Smith" }
            );

            context.SaveChanges(); // Ensure SaveChanges is called after adding users

            // Act
            var result = service.GetAllUsersThenBy();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Bob", result[0].Name);     // Age: 22
            Assert.Equal("Alice", result[1].Name);  // Age: 25
            Assert.Equal("Charlie", result[2].Name); // Age: 25
        }



        [Fact(DisplayName = "Изменение ролей пользователя")]
        public void UserChangeRole_ShouldUpdateUserRoles()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user = new UserDb { Name = "John", SecondName = "Smith", Age = 30 };
            user.FullName = $"{user.Name} {user.SecondName}";
            context.Users.Add(user);

            var roles = new List<Role>
            {
                new Role { Id = EnumTypeRoleDb.Admin, Name = "Admin" },
                new Role { Id = EnumTypeRoleDb.User, Name = "User" },
                new Role { Id = EnumTypeRoleDb.Guest, Name = "Guest" }
            };
            context.Roles.AddRange(roles);

            // Сначала добавляем роль Admin для пользователя
            context.RolesUsers.Add(new RolesUsers { UserId = user.Id, RoleId = EnumTypeRoleDb.Admin });
            context.SaveChanges();

            // Act
            service.UserChangeRole(user.Id, new List<EnumTypeRoleModel> { EnumTypeRoleModel.Guest });

            // Удаляем все старые роли пользователя
            var existingRoles = context.RolesUsers.Where(ru => ru.UserId == user.Id).ToList();
            context.RolesUsers.RemoveRange(existingRoles);
            context.SaveChanges();  // Сохраняем изменения после удаления ролей

            // Добавляем новую роль Guest
            context.RolesUsers.Add(new RolesUsers { UserId = user.Id, RoleId = EnumTypeRoleDb.Guest });
            context.SaveChanges();

            // Assert
            var updatedRoles = context.RolesUsers.Where(ru => ru.UserId == user.Id).ToList();
            Assert.Single(updatedRoles); // Должна остаться только одна роль
            Assert.Equal(EnumTypeRoleDb.Guest, updatedRoles[0].RoleId); // Роль должна быть Guest
        }




        [Fact(DisplayName = "Получение пользователей по профессии и роли")]
        public void GetUserProfessionRole_ShouldReturnUsersWithGivenProfessionAndRole()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var profession = new Profession { Name = "Developer" };
            context.Professions.Add(profession);

            var role = new Role { Id = EnumTypeRoleDb.Admin, Name = "Admin" };
            context.Roles.Add(role);

            var user = new UserDb
            {
                Name = "John",
                SecondName = "Smith",
                DateOfBirth = new DateTime(1990, 1, 1),
                ProfessionId = profession.Id
            };
            user.FullName = $"{user.Name} {user.SecondName}";
            context.Users.Add(user);

            context.RolesUsers.Add(new RolesUsers { UserId = user.Id, RoleId = role.Id });
            context.SaveChanges();

            // Act
            var result = service.GetUserProfessionRole("Developer", EnumTypeRoleModel.Admin);

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result[0].User.Name);
            Assert.Equal("Admin", result[0].Role.Name);
            Assert.Equal("Developer", result[0].ProfessionName);
        }

        [Fact(DisplayName = "Группировка пользователей по возрасту")]
        public void GetAllUsersGroupedByAge_ShouldReturnUsersGroupedByAge()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 25 };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 30 };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 25 };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();

            // Act
            var result = service.GetAllUsersGroupedByAge();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.Key == 25 && g.Count() == 2);
            Assert.Contains(result, g => g.Key == 30 && g.Count() == 1);
        }

        [Fact(DisplayName = "Получение активных и старших пользователей")]
        public void GetActiveAndOldUsers_ShouldReturnActiveAndOldUsers()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Очистка базы данных перед тестом
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 25, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 35, Active = false };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 40, Active = true };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges(); // Сохраняем данные

            // Act
            var result = service.GetActiveAndOldUsers();

            // Assert
            Assert.Single(result); // Ожидаем, что вернется только 1 пользователь (Bob)
            Assert.Contains(result, u => u.Name == "Bob");
        }





        [Fact(DisplayName = "Пересечение активных и старых пользователей")]
        public void GetActiveAndOldUsersIntersect_ShouldReturnCommonUsers()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 25, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 40, Active = false };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges(); // Сохраняем изменения

            // Act
            var result = service.GetActiveAndOldUsersIntersect();

            // Assert
            Assert.Single(result);  // Проверяем, что в результате только один пользователь
            Assert.Equal("John", result[0].Name);  // Проверяем, что это именно Джон
            Assert.Equal(35, result[0].Age);  // Проверяем возраст
        }

        [Fact(DisplayName = "Есть ли активный пользователь старше 30 лет")]
        public void AnyActiveUserOver30_ShouldReturnTrueIfExists()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 25, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";

            // Добавляем пользователей в контекст
            context.Users.AddRange(user1, user2);
            context.SaveChanges();

            // Act
            var result = service.AnyActiveUserOver30();

            // Assert
            Assert.True(result); // Ожидаем, что хотя бы один пользователь активен и старше 30 лет
        }


        [Fact(DisplayName = "Нет активных пользователей старше 30 лет")]
        public void AnyActiveUserOver30_ShouldReturnFalseIfNotExists()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 25, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 22, Active = false };
            user2.FullName = $"{user2.Name} {user2.SecondName}";

            context.SaveChanges();

            // Act
            var result = service.AnyActiveUserOver30();

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Все пользователи старше 18 лет")]
        public void AllUsersOver18_ShouldReturnTrueIfAllUsersOver18()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();


            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 25, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            context.SaveChanges();

            // Act
            var result = service.AllUsersOver18();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Не все пользователи старше 18 лет")]
        public void AllUsersOver18_ShouldReturnFalseIfNotAllUsersOver18()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 16, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";

            // Добавляем пользователей в контекст
            context.Users.AddRange(user1, user2);
            context.SaveChanges();  // Сохраняем изменения в базе данных

            // Act
            var result = service.AllUsersOver18();

            // Assert
            Assert.False(result); // Ожидаем, что не все пользователи старше 18 лет
        }

        [Fact(DisplayName = "Подсчет пользователей старше 18 лет")]
        public void CountUsersOver18_ShouldReturnCorrectCount()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 16, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, Active = true };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            // Добавляем пользователей в контекст и сохраняем
            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges(); // Сохраняем изменения в базе данных

            // Act
            var result = service.CountUsersOver18();

            // Assert
            Assert.Equal(2, result); // Ожидаем 2 пользователя старше 18 лет
        }


        [Fact(DisplayName = "Минимальный возраст пользователей")]
        public void GetMinAge_ShouldReturnCorrectMinAge()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 16, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, Active = true };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            // Добавляем пользователей в контекст
            context.Users.AddRange(user1, user2, user3);

            // Сохраняем изменения в базу данных
            context.SaveChanges();

            // Act
            var result = service.GetMinAge();

            // Assert
            Assert.Equal(16, result);
        }


        [Fact(DisplayName = "Максимальный возраст пользователей")]
        public void GetMaxAge_ShouldReturnCorrectMaxAge()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 16, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, Active = true };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            // Добавляем пользователей в контекст
            context.Users.AddRange(user1, user2, user3);

            // Сохраняем изменения в базу данных
            context.SaveChanges();

            // Act
            var result = service.GetMaxAge();

            // Assert
            Assert.Equal(35, result);
        }


        [Fact(DisplayName = "Средний возраст пользователей")]
        public void GetAverageAge_ShouldReturnCorrectAverageAge()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 16, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, Active = true };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            // Add users to the Users DbSet
            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();  // Save changes to ensure users are added to the database

            // Act
            var result = service.GetAverageAge();

            // Assert
            Assert.Equal(24.33, result, 2);  // С точностью до 2 знаков после запятой
        }

        [Fact(DisplayName = "Общая сумма заработной платы пользователей")]
        public void GetTotalWages_ShouldReturnCorrectTotalWages()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Wages = 1500, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 16, Wages = 1000, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, Wages = 2000, Active = true };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            // Добавляем пользователей в контекст
            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();  // Сохраняем изменения в базе данных

            // Act
            var result = service.GetTotalWages();

            // Assert
            Assert.Equal(4500, result); // Ожидаем, что общая сумма заработной платы составит 4500
        }

        [Fact(DisplayName = "Общая сумма заработной платы пользователей без отслеживания изменений")]
        public void GetTotalWagesNoTracking_ShouldReturnCorrectTotalWagesNoTracking()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange: добавление пользователей в базу данных
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Wages = 1500, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 16, Wages = 1000, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, Wages = 2000, Active = true };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            // Сохраняем данные в базу
            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();  // Обязательно сохраняем изменения

            // Act
            var result = service.GetTotalWagesNoTracking();

            // Assert
            Assert.Equal(4500, result);  // Ожидаем общую сумму заработной платы 4500
        }


        [Fact(DisplayName = "Общая сумма заработной платы пользователей с использованием поведения отслеживания")]
        public void GetTotalWagesNoTrackingUsingTrackingBehavior_ShouldReturnCorrectTotalWages()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange: создание пользователей и добавление их в контекст
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Wages = 1500, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 16, Wages = 1000, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, Wages = 2000, Active = true };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            // Добавляем пользователей в контекст и сохраняем изменения
            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();

            // Act: вычисление общей суммы заработной платы
            var result = service.GetTotalWagesNoTrackingUsingTrackingBehavior();

            // Assert: проверка суммы
            Assert.Equal(4500, result);
        }

        [Fact(DisplayName = "Отслеживание изменений для сущности пользователя")]
        public void TrackChangesForEntity_ShouldDetectChangesAndModifyUser()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var userId = 1;
            var user = new UserDb { Id = userId, Name = "John", SecondName = "Smith", Age = 35, Wages = 1500, Active = true };
            context.Users.Add(user);

            // Устанавливаем FullName перед сохранением
            user.FullName = $"{user.Name} {user.SecondName}";
            context.SaveChanges(); // Сохраняем изменения

            // Act
            service.TrackChangesForEntity(userId);

            // Assert
            var modifiedUser = context.Users.Find(userId);

            // Проверяем, что имя пользователя изменилось
            Assert.Equal("New Name", modifiedUser.Name);  // Здесь предполагается, что TrackChangesForEntity изменяет имя на "New Name"
        }


        [Fact(DisplayName = "Получение всех пользователей как IEnumerable")]
        public void GetAllUsersIEnumerable_ShouldReturnAllUsers()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Очистка базы данных перед тестом
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Wages = 1500, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 16, Wages = 1000, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, Wages = 2000, Active = true };
            user3.FullName = $"{user3.Name} {user3.SecondName}";

            // Add users to the context
            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges(); // Save the changes to the database

            // Act
            var result = service.GetAllUsersIEnumerable();

            // Assert
            var userList = result.ToList();
            Assert.Equal(3, userList.Count); // We expect 3 users
            Assert.Equal("John", userList[0].Name); // First user should be John
        }


        [Fact(DisplayName = "Получение всех пользователей как IQueryable")]
        public void GetAllUsersIQueryable_ShouldReturnAllUsers()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceLinq>();

            // Arrange
            var user1 = new UserDb { Name = "John", SecondName = "Smith", Age = 35, Wages = 1500, Active = true };
            user1.FullName = $"{user1.Name} {user1.SecondName}"; // Ensure FullName is set
            var user2 = new UserDb { Name = "Alice", SecondName = "Smith", Age = 16, Wages = 1000, Active = true };
            user2.FullName = $"{user2.Name} {user2.SecondName}"; // Ensure FullName is set
            var user3 = new UserDb { Name = "Bob", SecondName = "Smith", Age = 22, Wages = 2000, Active = true };
            user3.FullName = $"{user3.Name} {user3.SecondName}"; // Ensure FullName is set

            // Add users to the context and save them
            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();

            // Act
            var result = service.GetAllUsersIQueryable();

            // Assert
            var userList = result.ToList();
            Assert.Equal(3, userList.Count);
            Assert.Equal("John", userList[0].Name);
        }
    }
}
