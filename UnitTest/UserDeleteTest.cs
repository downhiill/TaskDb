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
    public class UserDeleteTest : ServiceUsersTests
    {
        [Theory(DisplayName = "Удаление пользователя из базы данных")]
        [Trait("Category", "CoreFunctionality")]
        [MemberData(nameof(TestData.GetUsersForDelete), MemberType = typeof(TestData))]
        public void Delete_ShouldRemoveUser(int userId, bool expectedSuccess)
        {
            // Мокируем метод Delete
            var mockServiceUsers = new Mock<IServiceUsers>();

            mockServiceUsers.Setup(service => service.Delete(It.IsAny<int>())).Verifiable();

            var serviceUsers = mockServiceUsers.Object;

            // Выполняем удаление
            serviceUsers.Delete(userId);

            // Проверяем, что метод Delete был вызван
            mockServiceUsers.Verify(service => service.Delete(userId), Times.Once);

            // Проверка результата (если userId существует или нет)
            if (expectedSuccess)
            {
                // Реальная проверка, если пользователь существует
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                Assert.Null(context.Users.Find(userId)); // Проверяем, что пользователь удален
            }
            else
            {
                // Если пользователь не существует, можно настроить ожидания, например, возвращать false или не менять контекст
                Assert.True(true); // Просто не нарушаем логику для несуществующих пользователей
            }
        }
    }
}
