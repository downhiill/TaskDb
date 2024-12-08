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
        [MemberData(nameof(TestData.AllUsers), MemberType = typeof(TestData))]
        public void Add_ShouldAddUser(UserModel user, bool expectedSuccess)
        {
            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

            if (!expectedSuccess)
            {
                // Ожидаем, что выбрасывается одно из исключений (ArgumentException или InvalidOperationException)
                var exception = Assert.ThrowsAny<Exception>(() => service.Add(user));

                if (user.Name == string.Empty || string.IsNullOrWhiteSpace(user.Name))
                {
                    // Если имя пустое, проверяем, что выбрасывается ArgumentException
                    Assert.IsType<ArgumentException>(exception);
                    Assert.Equal("User name cannot be empty or whitespace.", exception.Message);
                }
                else
                {
                    // Если имя не пустое, проверяем, что выбрасывается InvalidOperationException
                    Assert.IsType<InvalidOperationException>(exception);
                    Assert.Equal($"A user with the name '{user.Name}' already exists.", exception.Message);
                }
            }
            else
            {
                // Для корректных данных проверяем успешное добавление
                int userId = service.Add(user);
                Assert.True(userId > 0, "User should be successfully added.");
            }
        }

    }
}
