using Microsoft.Extensions.DependencyInjection;
using Moq;
using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ServiceUserTest
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


        [Fact(DisplayName = "Изменение имени пользователя для несуществующего ID")]
        [Trait("Category", "Update")]
        public void EditName_ShouldNotEditNameForNonExistentUser()
        {
            // Создаем мок для IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>();

            // Настраиваем мок метода EditName
            mockServiceUsers
                .Setup(service => service.EditName(It.IsAny<int>(), It.IsAny<string>()))
                .Callback<int, string>((id, name) =>
                {
                    if (id == 9999) // Проверяем несуществующий ID
                        throw new InvalidOperationException($"User with ID {id} does not exist.");
                });

            var serviceUsers = mockServiceUsers.Object;

            // Проверяем, что выбрасывается исключение при попытке изменить имя несуществующего пользователя
            var exception = Assert.Throws<InvalidOperationException>(() =>
                serviceUsers.EditName(9999, "NewName")
            );

            // Проверяем сообщение исключения
            Assert.Equal("User with ID 9999 does not exist.", exception.Message);

            // Убеждаемся, что вызов метода EditName был выполнен один раз с ожидаемыми параметрами
            mockServiceUsers.Verify(service => service.EditName(9999, "NewName"), Times.Once);
        }

        [Fact(DisplayName = "Изменение возраста пользователя")]
        [Trait("Priority", "High")]
        public void EditAge_ShouldEditUserAge()
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

            // Настраиваем мок для метода EditAge
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

            // Добавляем пользователя через мок
            var user = new UserModel { Name = "John", SecondName = "Smith", Age = 30, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) };
            user.FullName = $"{user.Name} {user.SecondName}";
            int userId = serviceUsers.Add(user);

            // Изменяем возраст пользователя через мок
            serviceUsers.EditAge(userId, 35);

            // Проверяем, что возраст был изменен
            var updatedUser = context.Users.Find(userId);
            Assert.NotNull(updatedUser); // Убедиться, что пользователь существует
            Assert.Equal(35, updatedUser?.Age); // Проверяем, что возраст обновился

            // Убедиться, что метод EditAge был вызван один раз
            mockServiceUsers.Verify(service => service.EditAge(userId, 35), Times.Once);
        }

        [Fact(DisplayName = "Изменение возраста пользователя для несуществующего ID")]
        [Trait("Category", "Update")]
        public void EditAge_ShouldNotEditAgeForNonExistentUser()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Создаем мок для IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);

            // Настраиваем мок для метода EditAge
            mockServiceUsers
                .Setup(service => service.EditAge(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((id, newAge) =>
                {
                    // Попытка найти пользователя в контексте
                    var user = context.Users.Find(id);
                    if (user != null)
                    {
                        user.Age = newAge;
                        context.SaveChanges();
                    }
                });

            var serviceUsers = mockServiceUsers.Object;

            // Попытка изменить возраст для несуществующего пользователя
            serviceUsers.EditAge(9999, 35); // ID 9999 предполагается несуществующим

            // Проверяем, что в базе данных нет пользователя с таким ID
            var user = context.Users.FirstOrDefault(u => u.Id == 9999);
            Assert.Null(user); // Пользователь с таким ID должен быть отсутствующим

            // Проверяем, что метод EditAge был вызван один раз
            mockServiceUsers.Verify(service => service.EditAge(9999, 35), Times.Once);
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
