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

        [Fact(DisplayName = "Получение всех профессий пользователей из базы")]
        [Trait("Priority", "High")]
        public void GetAllProfessionsUsers_ShouldReturnAllUsersWithProfessions()
        {
            // Подготавливаем список пользователей с профессиями
            var users = new List<UserModel>
            {
                new UserModel { Name = "John", ProfessionModel = new ProfessionModel { Name = "Developer" }},
                new UserModel { Name = "Jane", ProfessionModel = new ProfessionModel { Name = "Manager" }}
            };

            // Настраиваем мок для метода GetAllProfessionsUsers
            _mockServiceUsers.Setup(service => service.GetAllProfessionsUsers()).Returns(users.Select(u => new ModelUserProfession
            {
                UserName = u.Name,
                ProfessionName = u.ProfessionModel.Name
            }).ToList());

            // Вызываем реальный метод GetAllProfessionsUsers через мок
            var result = _mockServiceUsers.Object.GetAllProfessionsUsers();

            // Проверяем, что количество пользователей с профессиями корректно
            Assert.Equal(2, result.Count);

            // Проверяем, что все пользователи и их профессии возвращаются правильно
            Assert.Contains(result, item => item.UserName == "John" && item.ProfessionName == "Developer");
            Assert.Contains(result, item => item.UserName == "Jane" && item.ProfessionName == "Manager");
        }

        [Fact(DisplayName = "Получение статистики по профессиям из базы")]
        [Trait("Priority", "High")]
        public void GetAllProfessionsStats_ShouldReturnProfessionStats()
        {
            // Подготавливаем список профессий
            var professions = new List<ProfessionModel>
            {
                new ProfessionModel { Name = "Developer", Users = new List<UserModel> { new UserModel(), new UserModel() }},
                new ProfessionModel { Name = "Manager", Users = new List<UserModel> { new UserModel() }}
            };

            // Настроим мок для метода GetAllProfessionsStats
            _mockServiceUsers.Setup(service => service.GetAllProfessionsStats()).Returns(professions.Select(p => new ModelProfessionStats
            {
                Name = p.Name,
                Count = p.Users.Count
            }).ToList());

            // Вызываем реальный метод GetAllProfessionsStats через мок
            var result = _mockServiceUsers.Object.GetAllProfessionsStats();

            // Проверяем, что количество профессий и количество пользователей в профессиях корректно
            Assert.Equal(2, result.Count);

            // Проверяем статистику по профессиям
            Assert.Contains(result, item => item.Name == "Developer" && item.Count == 2);
            Assert.Contains(result, item => item.Name == "Manager" && item.Count == 1);
        }


    }

}

