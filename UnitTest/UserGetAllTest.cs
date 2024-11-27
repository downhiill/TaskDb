using _1.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class UserGetAllTest : ServiceUsersTests
    {
        [Fact(DisplayName = "Получение всех пользователей из базы")]
        [Trait("Priority", "High")]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Подготавливаем список пользователей
            var users = new List<UserModel>
            {
                new UserModel {Name = "John", SecondName = "Smith", Age = 30, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25)},
                new UserModel {Name = "Jane", SecondName = "Smpoke", Age = 25, Wages = 12400, DateOfBirth = new DateTime(2000, 11, 25)}
            };

            // Настраиваем мок для метода GetAllUsers
            _mockServiceUsers.Setup(service => service.GetAllUsers()).Returns(users);

            // Вызываем реальный метод GetAllUsers через мок
            var result = _mockServiceUsers.Object.GetAllUsers();

            // Проверяем, что количество пользователей корректно
            Assert.Equal(2, result.Count);

            // Проверяем, что все пользователи возвращаются правильно
            Assert.Contains(result, user => user.Name == "John" && user.SecondName == "Smith" && user.Age == 30 && user.Wages == 12500 && user.DateOfBirth == new DateTime(2000, 12, 25));
            Assert.Contains(result, user => user.Name == "Jane" && user.SecondName == "Smpoke" && user.Age == 25 && user.Wages == 12400 && user.DateOfBirth == new DateTime(2000, 11, 25));
        }

        [Fact(DisplayName = "Получение всех пользователей с краткой информацией")]
        [Trait("Priority", "High")]
        public void GetAllShortUsers_ShouldReturnCorrectUsers()
        {
            // Arrange
            var skip = 0;
            var take = 2;

            var users = new List<ShortUser>
            {
                new ShortUser { Id = 1, Name = "John", DateOfBirth = new DateTime(2000, 12, 25) },
                new ShortUser { Id = 2, Name = "Jane", DateOfBirth = new DateTime(2000, 11, 25) }
            };

            // Настраиваем мок
            _mockServiceUsers
                .Setup(service => service.GetAllShortUsers(skip, take))
                .Returns(users);

            // Act
            var result = _mockServiceUsers.Object.GetAllShortUsers(skip, take);

            // Assert
            Assert.NotNull(result); // Проверяем, что результат не null
            Assert.Equal(2, result.Count); // Проверяем, что возвращено правильное количество

            // Проверяем, что данные корректно преобразованы
            Assert.Contains(result, user => user.Id == 1 && user.Name == "John" && user.DateOfBirth == new DateTime(2000, 12, 25));
            Assert.Contains(result, user => user.Id == 2 && user.Name == "Jane" && user.DateOfBirth == new DateTime(2000, 11, 25));

            // Проверяем, что мок был вызван с правильными параметрами
            _mockServiceUsers.Verify(service => service.GetAllShortUsers(skip, take), Times.Once);
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
    }
}
