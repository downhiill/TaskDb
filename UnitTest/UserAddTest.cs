using _1.Tests;
using Microsoft.Extensions.DependencyInjection;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class UserAddTest : ServiceUsersTests
    {
        [Theory(DisplayName = "Добавление пользователя в базу данных")]
        [Trait("Category", "Critical")]
        [MemberData(nameof(TestData.GetUsersForAdd), MemberType = typeof(TestData))]
        public void Add_ShouldAddUser(UserModel user, bool expectedSuccess)
        {
            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

            // Добавляем пользователя
            int userId = service.Add(user);

            // Проверяем, что результат соответствует ожиданиям
            if (expectedSuccess)
            {
                Assert.True(userId > 0); // Пользователь должен быть добавлен
            }
            else
            {
                Assert.Equal(0, userId); // Пользователь не должен быть добавлен
            }
        }
    }
}
