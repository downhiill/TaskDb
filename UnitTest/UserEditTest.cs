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
                    var userDb = new UserDb { Name = user.Name, Age = user.Age, Wages = user.Wages, DateOfBirth = user.DateOfBirth, Active = user.Active, DateCreate = user.DateCreate };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id; // Возвращаем ID добавленного пользователя
                });

            // Создаем тестового пользователя
            var user = new UserModel { Name = originalName, Age = 30, Wages = 12500, DateOfBirth = new DateTime(2001, 12, 13) };
            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователя через мок
            int userId = serviceUsers.Add(user);

            // Редактируем имя пользователя через реальный сервис
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditName(userId, newName);

            // Проверяем, что имя пользователя было изменено
            var updatedUser = context.Users.Find(userId);
            Assert.NotNull(updatedUser); // Убеждаемся, что пользователь существует
            Assert.Equal(newName, updatedUser?.Name); // Проверяем новое имя
        }

        [Fact(DisplayName = "Изменение имени пользователя для несуществующего ID")]
        [Trait("Category", "Update")]
        public void EditName_ShouldNotEditNameForNonExistentUser()
        {
            // Создаем скоуп для работы с контекстом
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Попытка изменить имя для пользователя с несуществующим ID
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditName(9999, "NewName"); // Предположим, что ID 9999 не существует

            // Проверяем, что в базе данных нет пользователя с таким ID
            var user = context.Users.FirstOrDefault(u => u.Id == 9999);
            Assert.Null(user); // Пользователь с таким ID не должен существовать
        }

        [Fact(DisplayName = "Изменение возраста пользователя")]
        [Trait("Priority", "High")]
        public void EditAge_ShouldEditUserAge()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Создаем мок для IServiceUsers и перезаписываем только метод Add
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb { Name = user.Name, Age = user.Age, Wages = user.Wages, DateOfBirth = user.DateOfBirth, Active = user.Active, DateCreate = user.DateCreate };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id;
                });

            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователя через мок
            var user = new UserModel { Name = "John", Age = 30, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) };
            int userId = serviceUsers.Add(user);

            // Изменяем возраст пользователя через реальный сервис
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditAge(userId, 35);

            // Проверяем, что возраст был изменен
            var updatedUser = context.Users.Find(userId);
            Assert.NotNull(updatedUser);  // Убедиться, что пользователь существует
            Assert.Equal(35, updatedUser?.Age); // Проверяем, что возраст обновился
        }

        [Fact(DisplayName = "Изменение возраста пользователя для несуществующего ID")]
        [Trait("Category", "Update")]
        public void EditAge_ShouldNotEditAgeForNonExistentUser()
        {
            // Создаем скоуп для работы с контекстом
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Попытка изменить возраст для пользователя с несуществующим ID
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditAge(9999, 35); // Предположим, что ID 9999 не существует

            // Проверяем, что возраст пользователя не был изменен (пользователь с таким ID не существует)
            var user = context.Users.FirstOrDefault(u => u.Id == 9999);
            Assert.Null(user); // Пользователь с таким ID должен быть отсутствующим
        }

        [Fact(DisplayName = "Изменение зарплаты пользователя")]
        [Trait("Priority", "High")]
        public void EditWages_ShouldEditUserWages()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Создаем мок для IServiceUsers и перезаписываем только метод Add
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb { Name = user.Name, Age = user.Age, Wages = user.Wages, DateOfBirth = user.DateOfBirth, Active = user.Active, DateCreate = user.DateCreate };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id;
                });

            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователя через мок
            var user = new UserModel { Name = "John", Age = 30, Wages = 125000, DateOfBirth = new DateTime(2000, 12, 15) };
            int userId = serviceUsers.Add(user);

            // Изменяем зарплату пользователя через реальный сервис
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditWages(userId, 126000);

            // Проверяем, что зарплата была изменена
            var updatedUser = context.Users.Find(userId);
            Assert.NotNull(updatedUser);  // Убедиться, что пользователь существует
            Assert.Equal(126000, updatedUser?.Wages); // Проверяем, что возраст обновился
        }

        [Fact(DisplayName = "Изменение дня рождения пользователя")]
        [Trait("Priority", "High")]
        public void EditDateOfBirth_ShouldEditUserDateOfBirth()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Создаем мок для IServiceUsers и перезаписываем только метод Add
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb { Name = user.Name, Age = user.Age, Wages = user.Wages, DateOfBirth = user.DateOfBirth, Active = user.Active, DateCreate = user.DateCreate };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id;
                });

            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователя через мок
            var user = new UserModel { Name = "John", Age = 30, Wages = 125000, DateOfBirth = new DateTime(2000, 12, 15) };
            int userId = serviceUsers.Add(user);

            // Изменяем зарплату пользователя через реальный сервис
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditDateOfBirth(userId, new DateTime(2001, 10, 13));

            // Проверяем, что зарплата была изменена
            var updatedUser = context.Users.Find(userId);
            Assert.NotNull(updatedUser);  // Убедиться, что пользователь существует
            Assert.Equal(new DateTime(2001, 10, 13), updatedUser?.DateOfBirth); // Проверяем, что возраст обновился
        }
    }
}
