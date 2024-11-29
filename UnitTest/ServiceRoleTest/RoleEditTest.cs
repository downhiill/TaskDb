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
        [Fact(DisplayName = "Изменение ролей пользователя")]
        [Trait("Category", "Critical")]
        public void UserChangeRole_ShouldUpdateUserRoles()
        {
            // Данные для теста
            int userId = 1;
            var roles = new List<EnumTypeRoleModel> { EnumTypeRoleModel.User, EnumTypeRoleModel.Admin };

            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceRoles>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Добавляем старую роль
            service.UserAddRole(userId, EnumTypeRoleModel.User);

            // Изменяем роли: удаляем старые и добавляем новые
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
            Assert.Equal(roles.Count, roleUsers.Count);
            Assert.Contains(roleUsers, ru => ru.RoleId == (EnumTypeRoleDb)EnumTypeRoleModel.User);
            Assert.Contains(roleUsers, ru => ru.RoleId == (EnumTypeRoleDb)EnumTypeRoleModel.Admin);
        }
    }
}
