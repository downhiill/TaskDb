using Microsoft.Extensions.DependencyInjection;
using Moq;
using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ServiceRoleTest
{
    public class RoleGetRolesUser : ServiceRoleTest
    {
        [Theory(DisplayName = "Получение ролей пользователя")]
        [Trait("Category", "Critical")]
        [InlineData(1, 2, new EnumTypeRoleModel[] { EnumTypeRoleModel.User, EnumTypeRoleModel.Admin })]
        [InlineData(2, 0, new EnumTypeRoleModel[] { })]
        public void GetRolesUser_ShouldReturnUserRoles(int userId, int expectedRoleCount, EnumTypeRoleModel[] expectedRoles)
        {
            // Создание мока для возвращаемых ролей
            var userRoles = expectedRoles.Select(role => new RoleModel { Id = role, Name = role.ToString() }).ToList();

            // Мокируем метод GetRolesUser
            _mockServiceRoles
                .Setup(service => service.GetRolesUser(It.IsAny<int>()))
                .Returns(userRoles); // Возвращаем мокаемый список ролей

            using var scope = _serviceProvider.CreateScope();
            var service = _mockServiceRoles.Object;  // Используем замоканный сервис

            // Получаем роли пользователя через мок
            var result = service.GetRolesUser(userId);

            // Проверяем, что количество ролей совпадает с ожидаемым
            Assert.Equal(expectedRoleCount, result.Count);

            // Проверяем, что все ожидаемые роли присутствуют в результатах
            foreach (var expectedRole in expectedRoles)
            {
                Assert.Contains(result, role => role.Id == expectedRole);
            }
        }
    }
}
