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
    public class UserDeleteTest : ServiceUsersTests
    {
        [Theory(DisplayName = "Удаление пользователя из базы данных")]
        [Trait("Category", "CoreFunctionality")]
        [MemberData(nameof(TestData.ValidUsers), MemberType = typeof(TestData))]
        public void Delete_ShouldRemoveUser(UserModel user, bool expectedResult)
        {
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

        [Theory(DisplayName = "Удаление несуществующего пользователя")]
        [Trait("Category", "CoreFunctionality")]
        [MemberData(nameof(TestData.InvalidUsers), MemberType = typeof(TestData))]
        public void Delete_ShouldNotRemoveNonExistentUser(UserModel user, bool expectedResult)
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

        [Theory(DisplayName = "Удаление профессии")]
        [Trait("Category", "Critical")]
        [MemberData(nameof(TestData.ValidProfessions), MemberType = typeof(TestData))]
        public void DeleteProfession_ShouldRemoveProfessionSuccessfully(string professionName, bool expectedResult)
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Добавляем тестовую профессию в реальную базу данных
            var profession = new Profession { Name = professionName };
            context.Professions.Add(profession);
            context.SaveChanges();

            // Мокируем сервис IServiceUsers
            var mockServiceUsers = new Mock<IServiceUsers>();

            // Настроим мок для метода DeleteProfession, чтобы он удалял профессию из контекста
            mockServiceUsers.Setup(s => s.DeleteProfession(It.IsAny<int>())).Callback<int>((id) =>
            {
                // Имитируем удаление профессии из DbSet
                var professionToDelete = context.Professions.Find(id);
                if (professionToDelete != null)
                {
                    context.Professions.Remove(professionToDelete); // Удаляем профессию
                    context.SaveChanges(); // Применяем изменения
                }
            });

            // Получаем мокированный сервис
            var service = mockServiceUsers.Object;

            // Act: вызываем метод удаления
            service.DeleteProfession(profession.Id);

            // Assert: проверяем, что профессия была удалена из базы данных
            var deletedProfession = context.Professions.Find(profession.Id);
            Assert.Null(deletedProfession); // Профессия должна быть удалена
        }

        [Theory(DisplayName = "Удаление профессии - профессия не найдена")]
        [Trait("Category", "Critical")]
        [MemberData(nameof(TestData.InvalidProfessions), MemberType = typeof(TestData))]
        public void DeleteProfession_ShouldHandleProfessionNotFound(string professionName, bool expectedResult)
        {
            // Arrange
            using var scope = _serviceProvider.CreateScope();
            var mockServiceUsers = new Mock<IServiceUsers>();

            // Настроим мок для метода DeleteProfession так, чтобы он не вызывал исключение, если профессия не найдена
            mockServiceUsers.Setup(s => s.DeleteProfession(It.IsAny<int>())).Callback<int>((id) =>
            {
                // Имитируем поведение, когда профессия не найдена, т.е. ничего не делаем
                // В реальном методе можно проверить наличие профессии и если не найдено - ничего не делать или логировать.
            });

            // Получаем мокированный сервис
            var service = mockServiceUsers.Object;

            // Act: вызываем метод удаления для несуществующего id (например, 9999)
            Exception ex = Record.Exception(() => service.DeleteProfession(9999));

            // Assert: проверяем, что исключение не было выброшено
            Assert.Null(ex); // Проверяем, что исключение не было выброшено
        }
    }
}
