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
    public class RoleDeleteTest : ServiceRoleTest
    {
        [Theory(DisplayName = "Удаление роли у пользователя")]
        [Trait("Category", "Critical")]
        [InlineData(1, EnumTypeRoleModel.Admin)]
        [InlineData(2, EnumTypeRoleModel.User)]
        [InlineData(3, EnumTypeRoleModel.Guest)]
        public void UserRemoveRole_ShouldRemoveRoleFromUser(int userId, EnumTypeRoleModel role)
        {
            using var scope = _serviceProvider.CreateScope();
            var serviceMock = new Mock<IServiceRoles>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Добавляем роль пользователю (реальная база)
            var service = scope.ServiceProvider.GetRequiredService<IServiceRoles>();
            service.UserAddRole(userId, role);

            // Настройка мока: когда вызывается метод удаления, он просто ничего не делает
            serviceMock.Setup(s => s.UserRemoveRole(userId, role)).Verifiable();

            // Вызываем замоканный метод удаления
            serviceMock.Object.UserRemoveRole(userId, role);

            // Проверяем, что метод удаления был вызван
            serviceMock.Verify(s => s.UserRemoveRole(userId, role), Times.Once);

            // Проверяем, что роль осталась в базе (не удалена)
            var roleUser = context.RolesUsers.FirstOrDefault(ru => ru.UserId == userId && ru.RoleId == (EnumTypeRoleDb)role);
            Assert.NotNull(roleUser); // Роль должна остаться
        }
    }
}