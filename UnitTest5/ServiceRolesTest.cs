using _1.Models.Context;
using _1.Models.Entities;
using _1.Services;
using Microsoft.EntityFrameworkCore;

namespace UnitTest5
{
    public class ServiceRolesTest
    {
        private readonly ApplicationContext _dbContext;
        private readonly ServiceRoles _serviceRoles;

        public ServiceRolesTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;

            _dbContext = new ApplicationContext(options);
            _serviceRoles = new ServiceRoles(_dbContext);
        }

        [Fact]
        public void UserAddRole_ShouldAddRoleToUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", SecondName = "Smith" };
            var role = EnumTypeRoles.Admin;
            user.FullName = $"{user.Name} {user.SecondName}";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            _serviceRoles.UserAddRole(user.Id, role);

            // Assert
            var roleUser = _dbContext.RolesUsers.FirstOrDefault(ru => ru.UserId == user.Id && ru.RoleId == role);
            Assert.NotNull(roleUser);
        }

        [Fact]
        public void UserChangeRole_ShouldUpdateUserRoles()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", SecondName = "Smith" };
            user.FullName = $"{user.Name} {user.SecondName}";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            var initialRole = EnumTypeRoles.User;
            var newRoles = new List<EnumTypeRoles> { EnumTypeRoles.Admin, EnumTypeRoles.User };

            _serviceRoles.UserAddRole(user.Id, initialRole);

            // Act
            _serviceRoles.UserChangeRole(user.Id, newRoles);

            // Assert
            var userRoles = _dbContext.RolesUsers.Where(ru => ru.UserId == user.Id).ToList();
            Assert.Equal(2, userRoles.Count);
            Assert.Contains(userRoles, ru => ru.RoleId == EnumTypeRoles.Admin);
            Assert.Contains(userRoles, ru => ru.RoleId == EnumTypeRoles.User);
        }

        [Fact]
        public void UserRemoveRole_ShouldRemoveRoleFromUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", SecondName = "Smith" };
            user.FullName = $"{user.Name} {user.SecondName}";
            var role = EnumTypeRoles.Admin;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            _serviceRoles.UserAddRole(user.Id, role);

            // Act
            _serviceRoles.UserRemoveRole(user.Id, role);

            // Assert
            var roleUser = _dbContext.RolesUsers.FirstOrDefault(ru => ru.UserId == user.Id && ru.RoleId == role);
            Assert.Null(roleUser);
        }
    }
}
