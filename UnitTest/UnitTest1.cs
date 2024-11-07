using Moq;
using Xunit;
using _1.Services;
using _1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _1.Tests
{
    public class ServiceUsersTests
    {
        private readonly Mock<ApplicationContext> _mockDbContext;
        private readonly ServiceUser.ServiceUsers _serviceUsers;

        public ServiceUsersTests()
        {
            _mockDbContext = new Mock<ApplicationContext>();
            _serviceUsers = new ServiceUser.ServiceUsers(); // Здесь мы предполагаем, что контекст используется внутри сервиса напрямую
        }

        [Fact]
        public void Add_ShouldAddUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", Age = 30 };
            var dbSetMock = new Mock<DbSet<User>>();
            _mockDbContext.Setup(db => db.Users).Returns(dbSetMock.Object);

            // Act
            _serviceUsers.Add(user);

            // Assert
            dbSetMock.Verify(d => d.Add(It.Is<User>(u => u.Id == user.Id && u.Name == user.Name && u.Age == user.Age)), Times.Once);
            _mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void EditName_ShouldEditUserName()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", Age = 30 };
            var newName = "Johnny";
            var dbSetMock = new Mock<DbSet<User>>();
            _mockDbContext.Setup(db => db.Users.Find(user.Id)).Returns(user);
            _mockDbContext.Setup(db => db.Users).Returns(dbSetMock.Object);

            // Act
            _serviceUsers.EditName(user.Id, newName);

            // Assert
            Assert.Equal(newName, user.Name);
            _mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void EditName_ShouldNotEditIfUserNotFound()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", Age = 30 };
            var newName = "Johnny";
            _mockDbContext.Setup(db => db.Users.Find(user.Id)).Returns((User)null);

            // Act
            _serviceUsers.EditName(user.Id, newName);

            // Assert
            Assert.NotEqual(newName, user.Name);
            _mockDbContext.Verify(db => db.SaveChanges(), Times.Never);
        }

        [Fact]
        public void EditAge_ShouldEditUserAge()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", Age = 30 };
            var newAge = 35;
            var dbSetMock = new Mock<DbSet<User>>();
            _mockDbContext.Setup(db => db.Users.Find(user.Id)).Returns(user);
            _mockDbContext.Setup(db => db.Users).Returns(dbSetMock.Object);

            // Act
            _serviceUsers.EditAge(user.Id, newAge);

            // Assert
            Assert.Equal(newAge, user.Age);
            _mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_ShouldRemoveUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", Age = 30 };
            var dbSetMock = new Mock<DbSet<User>>();
            _mockDbContext.Setup(db => db.Users.Find(user.Id)).Returns(user);
            _mockDbContext.Setup(db => db.Users).Returns(dbSetMock.Object);

            // Act
            _serviceUsers.Delete(user.Id);

            // Assert
            dbSetMock.Verify(d => d.Remove(It.Is<User>(u => u.Id == user.Id)), Times.Once);
            _mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_ShouldNotRemoveUserIfNotFound()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", Age = 30 };
            _mockDbContext.Setup(db => db.Users.Find(user.Id)).Returns((User)null);

            // Act
            _serviceUsers.Delete(user.Id);

            // Assert
            _mockDbContext.Verify(db => db.SaveChanges(), Times.Never);
        }

        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Age = 30 },
                new User { Id = 2, Name = "Jane", Age = 25 }
            };
            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockDbContext.Setup(db => db.Users).Returns(dbSetMock.Object);

            // Act
            var result = _serviceUsers.GetAllUsers();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void SearchUsersMoreAge_ShouldReturnUsersOlderThanGivenAge()
        {
            // Arrange
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Age = 30 },
                new User { Id = 2, Name = "Jane", Age = 25 }
            };
            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockDbContext.Setup(db => db.Users).Returns(dbSetMock.Object);

            // Act
            var result = _serviceUsers.SearchUsersMoreAge(26);

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }

        [Fact]
        public void SearchUsers_ShouldReturnUsersMatchingSearchTerm()
        {
            // Arrange
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Age = 30 },
                new User { Id = 2, Name = "Jane", Age = 25 }
            };
            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockDbContext.Setup(db => db.Users).Returns(dbSetMock.Object);

            // Act
            var result = _serviceUsers.SearchUsers("John");

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }
    }
}
