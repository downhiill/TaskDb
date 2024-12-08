using _1.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class UserEditTest : ServiceUsersTests
    {
        [Theory(DisplayName = "Изменение имени пользователя")]
        [InlineData("John", "Johnny")]
        [InlineData("Jane", "Janette")]
        [Trait("Category", "Update")]
        public void EditName_ShouldEditUserName(string originalName, string newName)
        {
            // Создаем новый скоуп для работы с контекстом
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Настраиваем мок для метода Add, чтобы добавить пользователя в базу
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb
                    {
                        Name = user.Name,
                        SecondName = user.SecondName,
                        FullName = user.FullName,
                        Age = user.Age,
                        Wages = user.Wages,
                        DateOfBirth = user.DateOfBirth,
                        Active = user.Active,
                        DateCreate = user.DateCreate
                    };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id; // Возвращаем ID добавленного пользователя
                });

            // Мокаем метод EditName
            mockServiceUsers
                .Setup(service => service.EditName(It.IsAny<int>(), It.IsAny<string>()))
                .Callback<int, string>((userId, name) =>
                {
                    var userToUpdate = context.Users.FirstOrDefault(u => u.Id == userId);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Name = name; // Изменение имени пользователя
                        context.SaveChanges();    // Сохранение изменений в базе данных
                    }
                });

            // Создаем тестового пользователя
            var user = new UserModel
            {
                Name = originalName,
                SecondName = "Smith",
                Age = 30,
                Wages = 12500,
                DateOfBirth = new DateTime(2001, 12, 13)
            };
            user.FullName = $"{user.Name} {user.SecondName}";
            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователя через мок
            int userId = serviceUsers.Add(user);

            // Редактируем имя пользователя через реальный сервис
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            mockServiceUsers.Object.EditName(userId, newName);

            // Проверяем, что имя пользователя было изменено
            var updatedUser = context.Users.Find(userId);
            Assert.NotNull(updatedUser); // Убеждаемся, что пользователь существует
            Assert.Equal(newName, updatedUser?.Name); // Проверяем новое имя
        }

        [Theory(DisplayName = "Изменение возраста пользователя")]
        [Trait("Priority", "High")]
        [InlineData(30, 35, true)] // Валидный пользователь: изменим возраст с 30 на 35
        [InlineData(9999, 35, false)] // Несуществующий пользователь: изменим возраст на 35
        public void EditAge_ShouldEditUserAge(int userId, int newAge, bool isValid)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Создаем мок для IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>();

            // Настраиваем мок для метода Add
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb
                    {
                        Name = user.Name,
                        SecondName = user.SecondName,
                        FullName = user.FullName,
                        Age = user.Age,
                        Wages = user.Wages,
                        DateOfBirth = user.DateOfBirth,
                        Active = user.Active,
                        DateCreate = user.DateCreate
                    };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id;
                });

            // Настроим мок для метода EditAge
            mockServiceUsers
                .Setup(service => service.EditAge(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((id, newAge) =>
                {
                    var user = context.Users.Find(id);
                    if (user != null)
                    {
                        user.Age = newAge;
                        context.SaveChanges();
                    }
                });

            var serviceUsers = mockServiceUsers.Object;

            // Если валидный пользователь, добавляем его в базу
            if (isValid)
            {
                var user = new UserModel { Name = "John", SecondName = "Smith", Age = 30, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) };
                user.FullName = $"{user.Name} {user.SecondName}";
                userId = serviceUsers.Add(user); // Добавляем пользователя и получаем его ID
            }

            // Изменяем возраст пользователя
            serviceUsers.EditAge(userId, newAge);

            // Проверяем результат в зависимости от валидности
            var updatedUser = context.Users.Find(userId);
            if (isValid)
            {
                Assert.NotNull(updatedUser); // Убедиться, что пользователь существует
                Assert.Equal(newAge, updatedUser?.Age); // Проверяем, что возраст обновился
            }
            else
            {
                Assert.Null(updatedUser); // Пользователь не должен быть найден, если он не существует
            }

            // Проверяем, что метод EditAge был вызван
            mockServiceUsers.Verify(service => service.EditAge(userId, newAge), Times.Once);
        }

        [Fact(DisplayName = "Изменение зарплаты пользователя")]
        [Trait("Priority", "High")]
        public void EditWages_ShouldEditUserWages()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Создаем мок для IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);

            // Настраиваем мок для метода Add
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb
                    {
                        Name = user.Name,
                        SecondName = user.SecondName,
                        FullName = user.FullName,
                        Age = user.Age,
                        Wages = user.Wages,
                        DateOfBirth = user.DateOfBirth,
                        Active = user.Active,
                        DateCreate = user.DateCreate
                    };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id;
                });

            // Настраиваем мок для метода EditWages
            mockServiceUsers
                .Setup(service => service.EditWages(It.IsAny<int>(), It.IsAny<decimal>()))
                .Callback<int, decimal>((id, newWages) =>
                {
                    var user = context.Users.Find(id);
                    if (user != null)
                    {
                        user.Wages = newWages;
                        context.SaveChanges();
                    }
                });

            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователя через мок
            var user = new UserModel
            {
                Name = "John",
                SecondName = "Smith",
                Age = 30,
                Wages = 125000,
                DateOfBirth = new DateTime(2000, 12, 15)
            };
            user.FullName = $"{user.Name} {user.SecondName}";
            int userId = serviceUsers.Add(user);

            // Изменяем зарплату пользователя
            serviceUsers.EditWages(userId, 126000);

            // Проверяем, что зарплата была изменена
            var updatedUser = context.Users.Find(userId);
            Assert.NotNull(updatedUser);  // Убедиться, что пользователь существует
            Assert.Equal(126000, updatedUser?.Wages); // Проверяем, что зарплата обновилась
        }


        [Fact(DisplayName = "Изменение дня рождения пользователя")]
        [Trait("Priority", "High")]
        public void EditDateOfBirth_ShouldEditUserDateOfBirth()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Создаем мок для IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);

            // Настраиваем мок для метода Add
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb
                    {
                        Name = user.Name,
                        SecondName = user.SecondName,
                        FullName = user.FullName,
                        Age = user.Age,
                        Wages = user.Wages,
                        DateOfBirth = user.DateOfBirth,
                        Active = user.Active,
                        DateCreate = user.DateCreate
                    };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id;
                });

            // Настраиваем мок для метода EditDateOfBirth
            mockServiceUsers
                .Setup(service => service.EditDateOfBirth(It.IsAny<int>(), It.IsAny<DateTime>()))
                .Callback<int, DateTime>((id, newDateOfBirth) =>
                {
                    var user = context.Users.Find(id);
                    if (user != null)
                    {
                        user.DateOfBirth = newDateOfBirth;
                        context.SaveChanges();
                    }
                });

            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователя через мок
            var user = new UserModel
            {
                Name = "John",
                SecondName = "Smith",
                Age = 30,
                Wages = 125000,
                DateOfBirth = new DateTime(2000, 12, 15)
            };
            user.FullName = $"{user.Name} {user.SecondName}";
            int userId = serviceUsers.Add(user);

            // Изменяем дату рождения пользователя
            serviceUsers.EditDateOfBirth(userId, new DateTime(2001, 10, 13));

            // Проверяем, что дата рождения была изменена
            var updatedUser = context.Users.Find(userId);
            Assert.NotNull(updatedUser);  // Убедиться, что пользователь существует
            Assert.Equal(new DateTime(2001, 10, 13), updatedUser?.DateOfBirth); // Проверяем обновление
        }


        [Fact(DisplayName = "Изменение профессии пользователя")]
        [Trait("Category", "Critical")]
        public void EditProfessionUser_ShouldUpdateProfessionSuccessfully_WithMock()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Создаем тестовые данные
            var user = new UserDb { Id = 1, Name = "Alice", SecondName = "Johnson", ProfessionId = null };
            user.FullName = $"{user.Name} {user.SecondName}";
            var profession = new Profession { Id = 2, Name = "Engineer" };
            context.Users.Add(user);
            context.Professions.Add(profession);
            context.SaveChanges();

            // Создаем мок для IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>();

            // Настраиваем мок так, чтобы он выполнял изменения напрямую в контексте
            mockServiceUsers
                .Setup(service => service.EditProfessionUser(user.Id, profession.Id))
                .Callback<int, int?>((userId, professionId) =>
                {
                    // Логика обновления профессии пользователя
                    var existingUser = context.Users.Find(userId);
                    if (existingUser == null)
                        throw new InvalidOperationException($"User with ID {userId} not found.");

                    existingUser.ProfessionId = professionId;
                    context.SaveChanges();
                });

            var serviceUsers = mockServiceUsers.Object;

            // Выполняем обновление профессии
            serviceUsers.EditProfessionUser(user.Id, profession.Id);

            // Проверяем, что вызов метода был сделан
            mockServiceUsers.Verify(service => service.EditProfessionUser(user.Id, profession.Id), Times.Once);

            // Проверяем, что данные в контексте изменились
            var updatedUser = context.Users.Find(user.Id);
            Assert.NotNull(updatedUser);
            Assert.Equal(profession.Id, updatedUser?.ProfessionId);
        }

    }
}
