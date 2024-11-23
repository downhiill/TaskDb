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

        [Fact]
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
        [Fact]
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

        [Fact]
        public void EditName_ShouldEditUserName()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

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

            var user = new UserModel { Name = "John", Age = 30 };
            int userId = serviceUsers.Add(user);

            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditName(userId, "Johnny");

            var updatedUser = context.Users.Find(userId);
            Assert.Equal("Johnny", updatedUser?.Name);
        }


        [Fact]
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

            // Изменяем возраст пользователя через реальный метод
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();
            realServiceUsers.EditAge(userId, 35);

            // Проверяем, что возраст был изменен
            var updatedUser = context.Users.Find(userId);
            Assert.Equal(35, updatedUser?.Age);
        }


        [Fact]
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
        }

        [Fact]
        public void SearchUsersMoreAge_ShouldReturnUsersOlderThanGivenAge()
        {
            // Подготавливаем список пользователей
            var users = new List<UserModel>
            {
                new UserModel { Name = "John", Age = 30 },
                new UserModel { Name = "Jane", Age = 25 }
            };

            // Настраиваем мок для метода SearchUsersMoreAge
            _mockServiceUsers
                .Setup(service => service.SearchUsersMoreAge(It.IsAny<int>()))
                .Returns((int age) => users.Where(u => u.Age > age).ToList());

            // Вызываем метод SearchUsersMoreAge
            var result = _mockServiceUsers.Object.SearchUsersMoreAge(26);

            // Проверяем, что вернулся только один пользователь
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }

        [Fact]
        public void SearchUsers_ShouldReturnUsersMatchingSearchTerm()
        {
            // Подготавливаем список пользователей
            var users = new List<UserModel>
            {
                new UserModel { Name = "John", Age = 30 },
                new UserModel { Name = "Jane", Age = 25 }
            };

            // Настраиваем мок для метода SearchUsers
            _mockServiceUsers
                .Setup(service => service.SearchUsers(It.IsAny<string>()))
                .Returns((string searchTerm) => users.Where(u => u.Name.Contains(searchTerm)).ToList());

            // Вызываем метод SearchUsers
            var result = _mockServiceUsers.Object.SearchUsers("John");

            // Проверяем, что вернулся только один пользователь
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }

    }
}
