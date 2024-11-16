using _1.Models.Context;
using _1.Models.Entities;
using _1.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest5
{
    public class ServiceProfessionTest
    {
        private readonly ApplicationContext _dbContext;
        private readonly ServiceProfession _serviceProfession;

        public ServiceProfessionTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;

            _dbContext = new ApplicationContext(options);
            _serviceProfession = new ServiceProfession(_dbContext);
        }

        [Fact]
        public void GetUserProfessionRole_ShouldReturnCorrectUsersWithRoleAndProfession()
        {
            // Arrange
            var profession = new Profession { Id = 1, Name = "Engineer" };

            // Пользователи
            var user1 = new User { Id = 1, Name = "John", SecondName = "Smith", DateOfBirth = DateTime.Now.AddYears(-30), Profession = profession, FullName = "John Smith" };
            var user2 = new User { Id = 2, Name = "Jane", SecondName = "Wolf", DateOfBirth = DateTime.Now.AddYears(-25), Profession = profession, FullName = "Jane Wolf" };

            // Создание ролей
            var roleAdmin = new Roles { Id = EnumTypeRoles.Admin, Name = "Admin" };
            var roleUser = new Roles { Id = EnumTypeRoles.User, Name = "User" };

            // Добавление сущностей в контекст
            _dbContext.Professions.Add(profession);
            _dbContext.Users.AddRange(user1, user2);

            // Проверка, существуют ли роли в контексте, чтобы избежать повторного добавления
            if (!_dbContext.Roles.Any(r => r.Id == roleAdmin.Id))
            {
                _dbContext.Roles.Add(roleAdmin);
            }
            if (!_dbContext.Roles.Any(r => r.Id == roleUser.Id))
            {
                _dbContext.Roles.Add(roleUser);
            }

            _dbContext.SaveChanges();

            // Присваиваем роли пользователям
            _dbContext.RolesUsers.Add(new RolesUsers { UserId = user1.Id, RoleId = roleAdmin.Id });
            _dbContext.RolesUsers.Add(new RolesUsers { UserId = user2.Id, RoleId = roleUser.Id });
            _dbContext.SaveChanges();

            // Act
            var result = _serviceProfession.GetUserProfessionRole("Engineer", EnumTypeRoles.Admin);

            // Assert
            Assert.Single(result);
            var userRole = result.First();
            Assert.Equal("John", userRole.User.Name);
            Assert.Equal(EnumTypeRoles.Admin, userRole.Role.Type);
            Assert.Equal("Engineer", userRole.ProfessionName);
        }


        [Fact]
        public void GetUserProfessionRole_ShouldReturnEmptyList_WhenNoMatchingRole()
        {
            // Arrange
            var profession = new Profession { Id = 1, Name = "Engineer" };
            var user = new User { Id = 1, Name = "John", SecondName = "Smith", DateOfBirth = DateTime.Now.AddYears(-30), Profession = profession };
            var roleUser = new Roles { Name = "User" };
            user.FullName = $"{user.Name} {user.SecondName}";

            _dbContext.Professions.Add(profession);
            _dbContext.Roles.Add(roleUser);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            var result = _serviceProfession.GetUserProfessionRole("Engineer", EnumTypeRoles.Admin);

            // Assert
            Assert.Empty(result);  // Since no user has "Admin" role
        }



    }
}
