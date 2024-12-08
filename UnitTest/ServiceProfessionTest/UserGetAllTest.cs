using Microsoft.Extensions.DependencyInjection;
using Project.Data;
using Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ServiceProfessionTest
{
    public class UserGetAllTest : ServiceProfessionTest
    {
        [Fact(DisplayName = "Получение пользователей по профессии и роли")]
        [Trait("Category", "Critical")]
        public void GetUserProfessionRole_ShouldReturnCorrectUsers()
        {
            // Данные для теста
            var professionName = "Engineer";
            var role = EnumTypeRoleModel.Admin;

            // Мокируем базу данных InMemory
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                // Проверяем наличие профессии в базе данных, добавляем если нет
                var profession = context.Professions.FirstOrDefault(p => p.Name == professionName);
                if (profession == null)
                {
                    profession = new Profession { Name = professionName };
                    context.Professions.Add(profession);
                }

                // Проверяем наличие роли в базе данных, добавляем если нет
                var roleDb = context.Roles.FirstOrDefault(r => r.Name == "Admin");
                if (roleDb == null)
                {
                    roleDb = new Role { Id = (EnumTypeRoleDb)role, Name = "Admin" };
                    context.Roles.Add(roleDb);
                }

                // Создаем пользователей и связываем с профессией и ролью
                var user1 = new UserDb { Id = 1, Name = "John", SecondName = "Smith", Profession = profession };
                user1.FullName = $"{user1.Name} {user1.SecondName}";
                var user2 = new UserDb { Id = 2, Name = "Alice", SecondName = "Smith", Profession = profession };
                user2.FullName = $"{user2.Name} {user2.SecondName}";
                var user3 = new UserDb { Id = 3, Name = "Bob", SecondName = "Smith", Profession = new Profession { Name = "Designer" } };
                user3.FullName = $"{user3.Name} {user3.SecondName}";

                // Связываем пользователей с ролью
                context.Users.AddRange(user1, user2, user3);
                context.RolesUsers.AddRange(
                    new RolesUsers { RoleId = roleDb.Id, UserId = user1.Id, Role = roleDb, UserDb = user1 },
                    new RolesUsers { RoleId = (EnumTypeRoleDb)EnumTypeRoleModel.User, UserId = user2.Id, Role = new Role { Id = (EnumTypeRoleDb)EnumTypeRoleModel.User, Name = "User" }, UserDb = user2 },
                    new RolesUsers { RoleId = roleDb.Id, UserId = user3.Id, Role = roleDb, UserDb = user3 }
                );

                // Сохраняем изменения
                context.SaveChanges();

                // Получаем сервис
                var service = scope.ServiceProvider.GetRequiredService<IServiceProfession>();

                // Выполняем метод, который нужно протестировать
                var result = service.GetUserProfessionRole(professionName, role);

                // Assert: Проверяем, что только один пользователь соответствует роли Admin и профессии Engineer
                Assert.Single(result); // Только один пользователь должен быть возвращен
                Assert.Equal("John", result.First().User.Name); // Пользователь должен быть John
                Assert.Equal("Admin", result.First().Role.Name); // Роль должна быть Admin
            }
        }

    }
}