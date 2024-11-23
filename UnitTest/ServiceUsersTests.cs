using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.IService;
using System;
using Xunit;

namespace _1.Tests
{
    public class ServiceUsersTests
    {
        private readonly IServiceUsers _serviceUsers;
        private readonly ServiceProvider _serviceProvider;

        public ServiceUsersTests()
        {
            // Создание нового DI контейнера с уникальной In-Memory базой данных для каждого теста
            _serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString())) // Уникальное имя базы данных для каждого теста
                .AddScoped<IServiceUsers, ServiceUser>()
                .BuildServiceProvider();

            _serviceUsers = _serviceProvider.GetRequiredService<IServiceUsers>();
        }

        [Fact]
        public void Add_ShouldAddUser()
        {
            var user = new UserModel { Name = "John", Age = 30 };

            // Используем один скоуп для добавления и чтения
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();


            // Добавляем пользователя
            int userId = _serviceUsers.Add(user);


            // Проверяем, что ID пользователя больше нуля (пользователь добавлен)
            Assert.True(userId > 0);

        }
        [Fact]
        public void Delete_ShouldRemoveUser()
        {
            var user = new UserModel { Name = "John", Age = 30 };
            int userId = _serviceUsers.Add(user);

            // Удаляем пользователя
            _serviceUsers.Delete(userId);

            // Проверяем, что пользователь был удален
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            Assert.Null(context.Users.Find(userId));
        }

        [Fact]
        public void EditName_ShouldEditUserName()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var serviceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

            // Добавляем пользователя
            var user = new UserModel { Name = "John", Age = 30 };
            int userId = serviceUsers.Add(user);

            // Изменяем имя пользователя
            serviceUsers.EditName(userId, "Johnny");

            // Проверяем, что имя пользователя было обновлено
            var updatedUser = context.Users.Find(userId);

            Console.WriteLine($"After SaveChanges: ID={updatedUser?.Id}, Name={updatedUser?.Name}, Age={updatedUser?.Age}");
            Assert.Equal("Johnny", updatedUser?.Name);
        }


        [Fact]
        public void EditAge_ShouldEditUserAge()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var serviceUsers = scope.ServiceProvider.GetRequiredService<IServiceUsers>();

            var user = new UserModel { Name = "John", Age = 30 };
            int userId = serviceUsers.Add(user);

            // Изменяем возраст
            serviceUsers.EditAge(userId, 35);


            Assert.Equal(35, context.Users.Find(userId)?.Age);
        }

        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            _serviceUsers.Add(new UserModel { Name = "John", Age = 30 });
            _serviceUsers.Add(new UserModel { Name = "Jane", Age = 25 });

            // Получаем всех пользователей
            var result = _serviceUsers.GetAllUsers();

            // Проверяем, что количество пользователей правильно
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void SearchUsersMoreAge_ShouldReturnUsersOlderThanGivenAge()
        {
            _serviceUsers.Add(new UserModel { Name = "John", Age = 30 });
            _serviceUsers.Add(new UserModel { Name = "Jane", Age = 25 });

            // Ищем пользователей старше 26 лет
            var result = _serviceUsers.SearchUsersMoreAge(26);

            // Ожидаем, что вернется один пользователь (John)
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }

        [Fact]
        public void SearchUsers_ShouldReturnUsersMatchingSearchTerm()
        {
            _serviceUsers.Add(new UserModel { Name = "John", Age = 30 });
            _serviceUsers.Add(new UserModel { Name = "Jane", Age = 25 });

            // Ищем пользователей по имени "John"
            var result = _serviceUsers.SearchUsers("John");

            // Ожидаем, что вернется один пользователь (John)
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }
    }
}
