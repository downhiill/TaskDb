using _1.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Data;
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
        [Fact(DisplayName = "Добавление пользователя в базу данных")]
        [Trait("Category", "Critical")]
        public void Add_ShouldAddUser()
        {
            var user = new UserModel 
            { 
                
                Name = "John", 
                SecondName = "Smith", 
                Age = 30, 
                Wages = 12500, 
                DateOfBirth = new DateTime(2000, 12, 25)
            };
            user.FullName = $"{user.Name} {user.SecondName}";

            // Используем один скоуп для добавления и чтения
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            // Добавляем пользователя
            int userId = _serviceProvider.GetRequiredService<IServiceUsers>().Add(user);

            // Проверяем, что ID пользователя больше нуля (пользователь добавлен)
            Assert.True(userId > 0);
        }



        [Theory(DisplayName = "Добавление пользователя с некорректной моделью")]
        [Trait("Category", "Critical")]
        [MemberData(nameof(TestData.InvalidUsers), MemberType = typeof(TestData))]
        public void Add_ShouldNotAddUserWithInvalidModel(UserModel invalidUser)
        {
            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

            int userId = service.Add(invalidUser);

            Assert.Equal(0, userId);
        }



        [Fact(DisplayName = "Добавление пользователя с уже существующим именем")]
        [Trait("Category", "Critical")]
        public void Add_ShouldNotAddUserWithDuplicateName()
        {
            var user1 = new UserModel { Name = "John", SecondName = "Smith", Age = 30, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) };
            user1.FullName = $"{user1.Name} {user1.SecondName}";
            var user2 = new UserModel { Name = "John", SecondName = "Smith", Age = 25, Wages = 12500, DateOfBirth = new DateTime(2000, 12, 25) };
            user2.FullName = $"{user2.Name} {user2.SecondName}";


            using var scope = _serviceProvider.CreateScope();
            var realServiceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

            // Добавляем первого пользователя
            int userId1 = realServiceUsers.Add(user1);
            Assert.NotEqual(0, userId1); // Проверяем, что первый пользователь добавлен

            // Попытка добавить пользователя с таким же именем
            int userId2 = realServiceUsers.Add(user2);
            Assert.Equal(0, userId2); // Проверяем, что второй пользователь не был добавлен
        }

        [Fact(DisplayName = "Добавление профессии в базу данных")]
        [Trait("Category", "Critical")]
        public void AddProfession_ShouldAddProfession()
        {
            string professionName = "Software Developer";

            // Используем один скоуп для операций
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var service = scope.ServiceProvider.GetRequiredService<IServiceUsers>(); // Ваш класс, который содержит AddProfession

            // Используем метод AddProfession для добавления профессии
            service.AddProfession(professionName);

            // Проверяем, что профессия добавлена
            var addedProfession = context.Professions.FirstOrDefault(p => p.Name == professionName);
            Assert.NotNull(addedProfession);
            Assert.Equal(professionName, addedProfession.Name);
        }

        [Fact(DisplayName = "Добавление профессии с пустым именем")]
        [Trait("Category", "Critical")]
        public void AddProfession_ShouldNotAddProfessionWithEmptyName()
        {
            string professionName = ""; // Пустое имя

            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceUsers>(); // Ваш класс, который содержит AddProfession

            // Ожидаем, что метод выбросит исключение при пустом имени
            var exception = Assert.Throws<ArgumentException>(() => service.AddProfession(professionName));
            Assert.Equal("Имя профессии не может быть пустым. (Parameter 'name')", exception.Message);
        }



        [Fact(DisplayName = "Добавление дублирующей профессии")]
        [Trait("Category", "Critical")]
        public void AddProfession_ShouldNotAddDuplicateProfession()
        {
            string professionName = "Software Developer";

            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServiceUsers>(); // Ваш класс, который содержит AddProfession

            // Добавляем первую профессию
            service.AddProfession(professionName);

            // Проверяем, что дублирование профессии вызовет исключение
            var exception = Assert.Throws<DbUpdateException>(() => service.AddProfession(professionName));
            Assert.Equal("Такая профессия уже существует.", exception.Message);
        }


    }
}
