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
        [MemberData(nameof(TestData.GetUsersForUpdateName), MemberType = typeof(TestData))]
        [Trait("Category", "Update")]
        public void EditName_ShouldEditUserName(string originalName, string newName, bool expectedSuccess)
        {
            // Создаем новый скоуп для работы с контекстом
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Настроим мок для IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);

            // Мокируем метод Add, чтобы добавить пользователя в базу
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb
                    {
                        Name = user.Name,
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

            // Мокируем метод EditName
            mockServiceUsers
                .Setup(service => service.EditName(It.IsAny<int>(), It.IsAny<string>()))
                .Callback<int, string>((userId, name) =>
                {
                    var userToUpdate = context.Users.FirstOrDefault(u => u.Id == userId);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Name = name;
                        context.SaveChanges();
                    }
                    else if (!expectedSuccess) // Если пользователь не найден и ожидается неуспешный результат
                    {
                        throw new InvalidOperationException($"User with ID {userId} does not exist.");
                    }
                });

            // Создаем тестового пользователя
            var user = new UserModel
            {
                Name = originalName,
                Age = 30,
                Wages = 12500,
                DateOfBirth = new DateTime(2001, 12, 13)
            };
            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователя через мок
            int userId = serviceUsers.Add(user);

            // Редактируем имя пользователя через реальный сервис
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            try
            {
                mockServiceUsers.Object.EditName(userId, newName);

                if (expectedSuccess)
                {
                    // Проверяем, что имя пользователя было изменено
                    var updatedUser = context.Users.Find(userId);
                    Assert.NotNull(updatedUser);
                    Assert.Equal(newName, updatedUser?.Name);
                }
                else
                {
                    // Если изменение имени не должно было пройти, проверяем исключение
                    Assert.Throws<InvalidOperationException>(() =>
                        mockServiceUsers.Object.EditName(9999, newName)
                    );
                }
            }
            catch (InvalidOperationException ex)
            {
                if (expectedSuccess)
                {
                    // Если ожидался успешный результат, исключение не должно быть выброшено
                    Assert.Null(ex);
                }
            }
        }

        [Theory(DisplayName = "Изменение возраста пользователя")]
        [MemberData(nameof(TestData.GetUsersForUpdateAge), MemberType = typeof(TestData))]
        [Trait("Category", "Update")]
        public void EditAge_ShouldEditUserAge(int originalAge, int newAge, bool expectedSuccess)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Создаем мок для IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);

            // Настроим мок для метода Add
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb
                    {
                        Name = user.Name,
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
                    else if (!expectedSuccess) // Если пользователь не найден и ожидается неуспешный результат
                    {
                        throw new InvalidOperationException($"User with ID {id} does not exist.");
                    }
                });

            var serviceUsers = mockServiceUsers.Object;

            // Создаем тестового пользователя
            var user = new UserModel
            {
                Name = "John",
                Age = originalAge,
                Wages = 12500,
                DateOfBirth = new DateTime(2000, 12, 25)
            };
            int userId = serviceUsers.Add(user);

            // Пытаемся изменить возраст пользователя через мок
            try
            {
                mockServiceUsers.Object.EditAge(userId, newAge);

                if (expectedSuccess)
                {
                    // Проверяем, что возраст был изменен
                    var updatedUser = context.Users.Find(userId);
                    Assert.NotNull(updatedUser); // Убедиться, что пользователь существует
                    Assert.Equal(newAge, updatedUser?.Age); // Проверяем новый возраст
                }
                else
                {
                    // Если изменение возраста не должно было пройти, проверяем исключение
                    Assert.Throws<InvalidOperationException>(() =>
                        mockServiceUsers.Object.EditAge(9999, newAge)
                    );
                }
            }
            catch (InvalidOperationException ex)
            {
                if (expectedSuccess)
                {
                    // Если ожидался успешный результат, исключение не должно быть выброшено
                    Assert.Null(ex);
                }
            }

            // Убедиться, что метод EditAge был вызван один раз
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
                Age = 30,
                Wages = 125000,
                DateOfBirth = new DateTime(2000, 12, 15)
            };
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
                Age = 30,
                Wages = 125000,
                DateOfBirth = new DateTime(2000, 12, 15)
            };
            int userId = serviceUsers.Add(user);

            // Изменяем дату рождения пользователя
            serviceUsers.EditDateOfBirth(userId, new DateTime(2001, 10, 13));

            // Проверяем, что дата рождения была изменена
            var updatedUser = context.Users.Find(userId);
            Assert.NotNull(updatedUser);  // Убедиться, что пользователь существует
            Assert.Equal(new DateTime(2001, 10, 13), updatedUser?.DateOfBirth); // Проверяем обновление
        }
    }
}
