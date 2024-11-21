using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Project.Data;
using Project.IService;
using Microsoft.Extensions.Configuration;

namespace _1.Tests
{
    public class ServiceUsersTests
    {
        private readonly IServiceUsers _serviceUsers;
        private readonly DbContextOptions<ApplicationContext> _options;

        public ServiceUsersTests()
        {
            // Ќастройка в пам€ти базы данных
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                        .UseInMemoryDatabase(databaseName: "TestDatabase")
                        .Options;

            // —оздание контекста через DI
            var context = new ApplicationContext(_options);

            // —оздание сервиса, использу€ сконфигурированный контекст
            _serviceUsers = new ServiceUser(context);  // ѕередаем ApplicationContext
        }

        [Fact]
        public void Add_ShouldAddUser()
        {
            // Arrange
            var user = new UserModel { Id = 1, Name = "John", Age = 30 };

            // Act
            _serviceUsers.Add(user);

            // Assert
            using (var context = new ApplicationContext(_options))  // ”казываем, что это тестовое окружение
            {
                var userDb = context.Users.FirstOrDefault(u => u.Id == 1);
                Assert.NotNull(userDb);
                Assert.Equal(user.Name, userDb.Name);
                Assert.Equal(user.Age, userDb.Age);
            }
        }

        [Fact]
        public void EditName_ShouldEditUserName()
        {
            // Arrange
            var user = new UserModel { Id = 1, Name = "John", Age = 30 };
            _serviceUsers.Add(user);
            var newName = "Johnny";

            // Act
            _serviceUsers.EditName(1, newName);

            // Assert
            using (var context = new ApplicationContext(_options))  // ”казываем, что это тестовое окружение
            {
                var userDb = context.Users.FirstOrDefault(u => u.Id == 1);
                Assert.NotNull(userDb);
                Assert.Equal(newName, userDb.Name);
            }
        }

        [Fact]
        public void EditAge_ShouldEditUserAge()
        {
            // Arrange
            var user = new UserModel { Id = 1, Name = "John", Age = 30 };
            _serviceUsers.Add(user);
            var newAge = 35;

            // Act
            _serviceUsers.EditAge(1, newAge);

            // Assert
            using (var context = new ApplicationContext(_options))  // ”казываем, что это тестовое окружение
            {
                var userDb = context.Users.FirstOrDefault(u => u.Id == 1);
                Assert.NotNull(userDb);
                Assert.Equal(newAge, userDb.Age);
            }
        }

        [Fact]
        public void Delete_ShouldRemoveUser()
        {
            // Arrange
            var user = new UserModel { Id = 1, Name = "John", Age = 30 };
            _serviceUsers.Add(user);

            // Act
            _serviceUsers.Delete(1);

            // Assert
            using (var context = new ApplicationContext(_options))  // ”казываем, что это тестовое окружение
            {
                var userDb = context.Users.FirstOrDefault(u => u.Id == 1);
                Assert.Null(userDb);
            }
        }

        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var user1 = new UserModel { Id = 1, Name = "John", Age = 30 };
            var user2 = new UserModel { Id = 2, Name = "Jane", Age = 25 };
            _serviceUsers.Add(user1);
            _serviceUsers.Add(user2);

            // Act
            var result = _serviceUsers.GetAllUsers();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void SearchUsersMoreAge_ShouldReturnUsersOlderThanGivenAge()
        {
            // Arrange
            var user1 = new UserModel { Id = 1, Name = "John", Age = 30 };
            var user2 = new UserModel { Id = 2, Name = "Jane", Age = 25 };
            _serviceUsers.Add(user1);
            _serviceUsers.Add(user2);

            // Act
            var result = _serviceUsers.SearchUsersMoreAge(26);

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }

        [Fact]
        public void SearchUsers_ShouldReturnUsersMatchingSearchTerm()
        {
            // Arrange
            var user1 = new UserModel { Id = 1, Name = "John", Age = 30 };
            var user2 = new UserModel { Id = 2, Name = "Jane", Age = 25 };
            _serviceUsers.Add(user1);
            _serviceUsers.Add(user2);

            // Act
            var result = _serviceUsers.SearchUsers("John");

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }
    }
}
