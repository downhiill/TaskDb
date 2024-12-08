using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Project.Data;
using Project.IService;
using System;
using UnitTest;
using Xunit;

namespace _1.Tests
{
    public class ServiceUsersTests
    {
        private readonly Mock<IServiceUsers> _mockServiceUsers;
        private readonly ServiceProvider _serviceProvider;

        public ServiceUsersTests()
        {
            // Инициализация мокированного интерфейса и DI контейнера
            _mockServiceUsers = new Mock<IServiceUsers>();

            _serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString())) // Уникальная база данных для каждого теста
                .AddScoped<IServiceUsers, ServiceUser>() // Используем реальный сервис в DI
                .BuildServiceProvider();
        }

        [Theory(DisplayName = "Добавление пользователей в базу данных")]
        [Trait("Category", "Critical")]
        [MemberData(nameof(AddTestData.AllUsers), MemberType = typeof(AddTestData))]
        public void Add_ShouldHandleVariousUsers(UserModel user, bool expectedSuccess)
        {
            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

            if (!expectedSuccess)
            {
                // Проверяем, что при неверных данных метод выбрасывает ArgumentException
                var exception = Assert.Throws<ArgumentException>(() => service.Add(user));
                Assert.Equal("User name cannot be empty or whitespace.", exception.Message);
            }
            else
            {
                // Для корректных данных проверяем успешное добавление
                int userId = service.Add(user);
                Assert.True(userId > 0, "User should be successfully added.");
            }
        }


        [Theory(DisplayName = "Удаление пользователя из базы данных")]
        [Trait("Category", "CoreFunctionality")]
        [MemberData(nameof(DeleteTestData.UserDeletionData), MemberType = typeof(DeleteTestData))]
        public void Delete_ShouldHandleUserDeletion(int userId, bool shouldExist)
        {
            // Мокируем метод Delete
            _mockServiceUsers.Setup(service => service.Delete(It.IsAny<int>())).Verifiable();

            // Настраиваем мок для существующего пользователя
            if (shouldExist)
            {
                var user = new UserDb { Id = userId, Name = "John", Age = 30 };
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.Users.Add(user);
                context.SaveChanges();
            }

            // Удаляем пользователя
            _mockServiceUsers.Object.Delete(userId);

            // Проверяем, что метод Delete был вызван
            _mockServiceUsers.Verify(service => service.Delete(userId), Times.Once);

            // Проверяем состояние базы
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                var user = context.Users.Find(userId);
                if (shouldExist)
                {
                    Assert.Null(user); // Пользователь должен быть удалён
                }
                else
                {
                    Assert.Null(user); // Пользователя и так не должно быть
                }
            }
        }

        [Theory(DisplayName = "Изменение имени пользователя")]
        [MemberData(nameof(EditTestData.ValidUpdateNames), MemberType = typeof(EditTestData))]
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
                    var userDb = new UserDb { Name = user.Name, Age = user.Age };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id; // Возвращаем ID добавленного пользователя
                });

            // Создаем тестового пользователя
            var user = new UserModel { Name = originalName, Age = 30 };
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

        [Theory(DisplayName = "Изменение имени пользователя для несуществующего ID")]
        [MemberData(nameof(EditTestData.NonExistentUserIds), MemberType = typeof(EditTestData))]
        [Trait("Category", "Update")]
        public void EditName_ShouldNotEditNameForNonExistentUser(int userId, string newName)
        {
            // Создаем скоуп для работы с контекстом
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Попытка изменить имя для пользователя с несуществующим ID
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditName(userId, newName); // Предположим, что ID 9999 не существует

            // Проверяем, что в базе данных нет пользователя с таким ID
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            Assert.Null(user); // Пользователь с таким ID не должен существовать
        }


        [Theory(DisplayName = "Изменение возраста пользователя")]
        [MemberData(nameof(EditTestData.ValidAgeUpdates), MemberType = typeof(EditTestData))]
        [Trait("Priority", "High")]
        public void EditAge_ShouldEditUserAge(string name, int currentAge, int newAge)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Создаем мок для IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb { Name = user.Name, Age = user.Age };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id;
                });

            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователя через мок
            var user = new UserModel { Name = name, Age = currentAge };
            int userId = serviceUsers.Add(user);

            // Изменяем возраст пользователя через реальный сервис
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditAge(userId, newAge);

            // Проверяем, что возраст был изменен
            var updatedUser = context.Users.Find(userId);
            Assert.NotNull(updatedUser);  // Убедиться, что пользователь существует
            Assert.Equal(newAge, updatedUser?.Age); // Проверяем, что возраст обновился
        }

        [Theory(DisplayName = "Изменение возраста пользователя для несуществующего ID")]
        [MemberData(nameof(EditTestData.NonExistentUserAgeUpdates), MemberType = typeof(EditTestData))]
        [Trait("Category", "Update")]
        public void EditAge_ShouldNotEditAgeForNonExistentUser(int userId, int newAge)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Попытка изменить возраст для пользователя с несуществующим ID
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditAge(userId, newAge); // Предположим, что ID не существует

            // Проверяем, что пользователь с таким ID отсутствует
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            Assert.Null(user); // Пользователь с таким ID должен быть отсутствующим
        }


        [Fact(DisplayName = "Получение всех пользователей из базы")]
        [Trait("Priority", "High")]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Подготавливаем список пользователей
            var users = new List<UserModel>
            {
                new UserModel { Name = "John", Age = 30 },
                new UserModel { Name = "Jane", Age = 25 }
            };

            // Настраиваем мок для метода GetAllUsers
            _mockServiceUsers.Setup(service => service.GetAllUsers()).Returns(users);

            // Вызываем реальный метод GetAllUsers через мок
            var result = _mockServiceUsers.Object.GetAllUsers();

            // Проверяем, что количество пользователей корректно
            Assert.Equal(2, result.Count);

            // Проверяем, что все пользователи возвращаются правильно
            Assert.Contains(result, user => user.Name == "John" && user.Age == 30);
            Assert.Contains(result, user => user.Name == "Jane" && user.Age == 25);
        }

        [Fact(DisplayName = "Получение всех пользователей, когда база данных пуста")]
        [Trait("Priority", "Low")]
        public void GetAllUsers_ShouldReturnEmptyListWhenNoUsers()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Проверяем, что в базе нет пользователей
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            var result = realServiceUsers.GetAllUsers();

            // Проверяем, что список пользователей пуст
            Assert.Empty(result);
        }


        [Theory(DisplayName = "Поиск пользователей старше указанного возраста")]
        [InlineData(25, 2)] // Возраст: 25, ожидаемое количество: 2
        [InlineData(30, 1)] // Возраст: 30, ожидаемое количество: 1
        [Trait("Category", "Search")]
        public void SearchUsersMoreAge_ShouldReturnUsersOlderThanGivenAge(int age, int expectedCount)
        {
            // Создаем новый скоуп для работы с контекстом
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Настраиваем мок для метода Add
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);
            mockServiceUsers
                .Setup(service => service.Add(It.IsAny<UserModel>()))
                .Returns((UserModel user) =>
                {
                    var userDb = new UserDb { Name = user.Name, Age = user.Age };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id;
                });

            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователей через мок
            serviceUsers.Add(new UserModel { Name = "John", Age = 30 });
            serviceUsers.Add(new UserModel { Name = "Jane", Age = 40 });
            serviceUsers.Add(new UserModel { Name = "Alice", Age = 20 });

            // Получаем реальный сервис для вызова метода поиска
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

            // Выполняем поиск пользователей старше указанного возраста
            var result = realServiceUsers.SearchUsersMoreAge(age);

            // Проверяем количество найденных пользователей
            Assert.Equal(expectedCount, result.Count);

            // Проверяем, что все найденные пользователи старше указанного возраста
            Assert.All(result, user => Assert.True(user.Age > age));
        }

        [Fact(DisplayName = "Поиск пользователей по имени")]
        [Trait("Priority", "High")]
        public void SearchUsers_ShouldReturnUsersMatchingSearchTerm()
        {
            // Создаем мок для IServiceUsers и перезаписываем только метод SearchUsers
            var mockServiceUsers = new Mock<IServiceUsers>(MockBehavior.Default);

            // Подготавливаем список пользователей
            var users = new List<UserModel>
            {
                new UserModel { Name = "John", Age = 30 },
                new UserModel { Name = "Jane", Age = 25 }
            };

            // Настройка мока для метода SearchUsers
            mockServiceUsers
                .Setup(service => service.SearchUsers(It.IsAny<string>()))
                .Returns((string searchTerm) => users.Where(u => u.Name.Contains(searchTerm)).ToList());

            var serviceUsers = mockServiceUsers.Object;

            // Вызываем метод SearchUsers с параметром "John"
            var result = serviceUsers.SearchUsers("John");

            // Проверяем, что вернулся только один пользователь
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }

        [Fact(DisplayName = "Поиск пользователя по имени, которого нет в базе")]
        [Trait("Category", "Search")]
        public void SearchUsers_ShouldReturnEmptyListWhenUserNotFound()
        {
            // Создаем мок для IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>();

            // Подготавливаем список пользователей
            var users = new List<UserModel>
            {
                new UserModel { Name = "John", Age = 30 },
                new UserModel { Name = "Jane", Age = 25 }
            };

            // Настроим мок для метода SearchUsers
            mockServiceUsers
                .Setup(service => service.SearchUsers(It.IsAny<string>()))
                .Returns((string searchTerm) => users.Where(u => u.Name.Contains(searchTerm)).ToList());

            var serviceUsers = mockServiceUsers.Object;

            // Ищем пользователя, которого нет в списке
            var result = serviceUsers.SearchUsers("Mike");

            // Проверяем, что вернулся пустой список
            Assert.Empty(result);
        }
    }
}
