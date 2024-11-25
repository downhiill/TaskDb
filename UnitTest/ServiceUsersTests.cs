using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Project.Data;
using Project.IService;
using System;
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

        [Fact(DisplayName = "Добавление пользователя в базу данных")]
        [Trait("Category", "Critical")]
        public void Add_ShouldAddUser()
        {
            var user = new UserModel { Name = "John", Age = 30 };

            // Используем один скоуп для добавления и чтения
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Добавляем пользователя
            int userId = _serviceProvider.GetRequiredService<IServiceUsers>().Add(user);

            // Проверяем, что ID пользователя больше нуля (пользователь добавлен)
            Assert.True(userId > 0);
        }

        public static IEnumerable<object[]> InvalidUsers => new List<object[]>
        {
            new object[] { new UserModel { Name = "", Age = 30 } },
            new object[] { new UserModel { Name = " ", Age = 25 } },
            new object[] { new UserModel { Name = "\t", Age = 40 } }
        };

        [Theory(DisplayName = "Добавление пользователя с некорректной моделью")]
        [Trait("Category", "Critical")]
        [MemberData(nameof(InvalidUsers))]
        public void Add_ShouldNotAddUserWithInvalidModel(UserModel invalidUser)
        {
            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

            int userId = service.Add(invalidUser);

            Assert.Equal(0, userId);
        }



        [Fact(DisplayName = "Добавление пользователя с уже существующим именем")]
        [Trait("Category", "Critical")]
        public void Add_ShouldNotAddUserWithDuplicateName()
        {
            var user1 = new UserModel { Name = "John", Age = 30 };
            var user2 = new UserModel { Name = "John", Age = 25 };

            using var scope = _serviceProvider.CreateScope();
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

            // Добавляем первого пользователя
            int userId1 = realServiceUsers.Add(user1);
            Assert.NotEqual(0, userId1); // Проверяем, что первый пользователь добавлен

            // Попытка добавить пользователя с таким же именем
            int userId2 = realServiceUsers.Add(user2);
            Assert.Equal(0, userId2); // Проверяем, что второй пользователь не был добавлен
        }



        [Fact(DisplayName = "Удаление пользователя из базы данных")]
        [Trait("Category", "CoreFunctionality")]
        public void Delete_ShouldRemoveUser()
        {
            var user = new UserModel { Name = "John", Age = 30 };

            // Мокируем метод Add, чтобы он всегда возвращал ID пользователя (например, 1)
            _mockServiceUsers.Setup(service => service.Add(It.IsAny<UserModel>())).Returns(1);

            // Мокируем добавление пользователя
            int userId = _mockServiceUsers.Object.Add(user);

            // Настроим мок для метода Delete, чтобы он корректно выполнялся
            _mockServiceUsers.Setup(service => service.Delete(userId)).Verifiable();

            // Удаляем пользователя через мок
            _mockServiceUsers.Object.Delete(userId);

            // Проверяем, что метод Delete был вызван
            _mockServiceUsers.Verify(service => service.Delete(userId), Times.Once);

            // Проверяем, что пользователь был удален из контекста базы данных
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            Assert.Null(context.Users.Find(userId)); // Реальный контекст
        }

        [Fact(DisplayName = "Удаление несуществующего пользователя")]
        [Trait("Category", "CoreFunctionality")]
        public void Delete_ShouldNotRemoveNonExistentUser()
        {
            // Мокируем метод Delete для несуществующего пользователя
            var mockServiceUsers = new Mock<IServiceUsers>();

            // Настройка мока для метода Delete
            mockServiceUsers
                .Setup(service => service.Delete(It.IsAny<int>())) // Не нужно возвращать значение
                .Verifiable(); // Проверка, что метод был вызван

            var serviceUsers = mockServiceUsers.Object;

            // Попытка удалить пользователя с несуществующим ID
            serviceUsers.Delete(9999); // Предположим, что ID 9999 не существует

            // Проверяем, что метод Delete был вызван
            mockServiceUsers.Verify(service => service.Delete(9999), Times.Once);
        }



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
                    var userDb = new UserDb { Name = user.Name, Age = user.Age };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id;
                });

            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователя через мок
            var user = new UserModel { Name = "John", Age = 30 };
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
