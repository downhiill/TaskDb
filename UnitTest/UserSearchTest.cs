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
    public class UserSearchTest : ServiceUsersTests
    {
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
                    var userDb = new UserDb { Name = user.Name, SecondName = user.SecondName, FullName = user.FullName, Age = user.Age, Wages = user.Wages, DateOfBirth = user.DateOfBirth };
                    context.Users.Add(userDb);
                    context.SaveChanges();
                    return userDb.Id;
                });

            var serviceUsers = mockServiceUsers.Object;

            // Добавляем пользователей через мок
            var user1 = new UserModel { Name = "John", SecondName = "Smith", Age = 30, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25)};
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            serviceUsers.Add(user1);
            var user2 = new UserModel { Name = "Jane", SecondName = "Smpoke", Age = 40, Wages = 12400, DateOfBirth = new DateTime(2000, 10, 25) };
            user2.FullName = $"{user2.Name} {user2.SecondName}";
            serviceUsers.Add(user2);
            var user3 = new UserModel { Name = "Alice", SecondName = "Wayn", Age = 20, Wages = 12300, DateOfBirth = new DateTime(2000, 11, 25) };
            user3.FullName = $"{user3.Name} {user3.SecondName}";
            serviceUsers.Add(user3);


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
                new UserModel {Name = "John", SecondName = "Smith", Age = 30, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25)},
                new UserModel {Name = "Jane", SecondName = "Wayn", Age = 25, Wages = 12400, DateOfBirth = new DateTime(2000, 11, 25)}
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
                new UserModel {Name = "John", SecondName = "Smith", Age = 30, Wages = 12400, DateOfBirth = new DateTime(2000, 10, 25)},
                new UserModel {Name = "Jane", SecondName = "Wayn", Age = 25, Wages = 12200, DateOfBirth = new DateTime(2000, 11, 25)}
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
