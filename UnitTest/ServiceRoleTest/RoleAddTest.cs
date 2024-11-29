using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ServiceRoleTest
{
    public class RoleAddTest : ServiceRoleTest
    {
        [Fact(DisplayName = "Добавление роли пользователю")]
        [Trait("Category", "Critical")]
        public void UserAddRole_ShouldAddRoleToUser()
        {
            // Данные для теста
            int userId = 1;
            var role = EnumTypeRoleModel.Admin;

            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceRoles>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Добавляем роль пользователю
            service.UserAddRole(userId, role);

            // Проверяем, что роль добавлена
            var roleUser = context.RolesUsers.FirstOrDefault(ru => ru.UserId == userId && ru.RoleId == (EnumTypeRoleDb)role);
            Assert.NotNull(roleUser);
        }
    }
}
