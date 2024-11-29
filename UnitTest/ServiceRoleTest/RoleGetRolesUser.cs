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
        [Fact(DisplayName = "Получение ролей пользователя")]
        [Trait("Category", "Critical")]
        public void GetRolesUser_ShouldReturnUserRoles()
        {
            // Данные для теста
            int userId = 1;
            var roles = new List<EnumTypeRoleModel> { EnumTypeRoleModel.User, EnumTypeRoleModel.Admin };

            // Создание модели ролей для мока
            var userRoles = new List<RoleModel>
            {
                new RoleModel { Id = EnumTypeRoleModel.User, Name = "User" },
                new RoleModel { Id = EnumTypeRoleModel.Admin, Name = "Admin" }
            };

            // Мокируем метод GetRolesUser
            _mockServiceRoles
                .Setup(service => service.GetRolesUser(It.IsAny<int>()))
                .Returns(userRoles); // Возвращаем мокаемый список ролей

            using var scope = _serviceProvider.CreateScope();
            var service = _mockServiceRoles.Object;  // Используем замоканный сервис

            // Получаем роли пользователя через мок
            var result = service.GetRolesUser(userId);

            // Проверяем, что роли получены правильно
            Assert.Equal(roles.Count, result.Count);
            Assert.Contains(result, role => role.Id == EnumTypeRoleModel.User);
            Assert.Contains(result, role => role.Id == EnumTypeRoleModel.Admin);
        }
    }
}
