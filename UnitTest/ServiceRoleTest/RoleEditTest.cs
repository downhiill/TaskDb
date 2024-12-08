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
    public class RoleEditTest : ServiceRoleTest
    {
        [Theory(DisplayName = "Изменение ролей пользователя")]
        [Trait("Category", "Critical")]
        [InlineData(1, new[] { EnumTypeRoleModel.User, EnumTypeRoleModel.Admin })]
        [InlineData(2, new[] { EnumTypeRoleModel.Guest, EnumTypeRoleModel.User })]
        [InlineData(3, new[] { EnumTypeRoleModel.Admin, EnumTypeRoleModel.Guest })]
        public void UserChangeRole_ShouldUpdateUserRoles(int userId, EnumTypeRoleModel[] roles)
        {
            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceRoles>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Удаляем старые роли пользователя, если они есть
            var oldRoles = context.RolesUsers.Where(ru => ru.UserId == userId).ToList();
            context.RolesUsers.RemoveRange(oldRoles);
            context.SaveChanges();

            // Добавляем новые роли
            foreach (var role in roles)
            {
                service.UserAddRole(userId, role);
            }

            // Проверяем, что роли обновлены
            var roleUsers = context.RolesUsers.Where(ru => ru.UserId == userId).ToList();
            Assert.Equal(roles.Length, roleUsers.Count);  // Проверка, что количество ролей совпадает

            // Проверяем, что каждая роль была добавлена
            foreach (var role in roles)
            {
                Assert.Contains(roleUsers, ru => ru.RoleId == (EnumTypeRoleDb)role);
            }
        }
    }
}
