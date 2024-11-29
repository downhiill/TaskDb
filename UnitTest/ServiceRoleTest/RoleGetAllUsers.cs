using Microsoft.Extensions.DependencyInjection;
using Project.Data;
using Project.IService;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ServiceRoleTest
{
    public class RoleGetAllUsers : ServiceRoleTest
    {
        [Fact(DisplayName = "Получение пользователей по роли из базы данных")]
        [Trait("Category", "Integration")]
        public async Task GetUsers_ShouldReturnUsersWithSpecifiedRole()
        {
            // Arrange
            var dbContext = _serviceProvider.GetRequiredService<ApplicationContext>();

            // Добавляем пользователей и роли в базу данных
            var role = new Role { Id = (EnumTypeRoleDb)1, Name = "Admin" };
            var user1 = new UserDb { Id = 1, Name = "John",SecondName="Smith", DateOfBirth = new DateTime(1990, 1, 1) };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserDb { Id = 2, Name = "Jane", SecondName = "Smith", DateOfBirth = new DateTime(1992, 2, 2) };
            user2.FullName = $"{user2.Name} {user2.SecondName}";

            dbContext.Roles.Add(role);
            dbContext.Users.AddRange(user1, user2);
            dbContext.RolesUsers.AddRange(
                new RolesUsers { RoleId = role.Id, UserId = user1.Id, Role = role, UserDb = user1 },
                new RolesUsers { RoleId = role.Id, UserId = user2.Id, Role = role, UserDb = user2 }
            );
            await dbContext.SaveChangesAsync();

            var service = _serviceProvider.GetRequiredService<IServiceRoles>();

            // Act
            var result = service.GetUsers(EnumTypeRoleModel.Admin);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);  // Ожидаем 2 пользователя с ролью Admin

            // Проверяем, что оба пользователя с ролью Admin
            Assert.Contains(result, u => u.User.Name == "John" && u.Role.Name == "Admin");
            Assert.Contains(result, u => u.User.Name == "Jane" && u.Role.Name == "Admin");
        }
    }
}
