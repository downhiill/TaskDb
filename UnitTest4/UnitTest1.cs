using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using static _1.Services.ServiceUser;
using _1.Models.Context;
using _1.Models.Entities;
namespace UnitTest4
{
    public class UnitTest1
    {
        private readonly ApplicationContext _dbContext;
        private readonly ServiceUsers _serviceUsers;

        public UnitTest1()
        {

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                 .Options;

            _dbContext = new ApplicationContext(options);  
            _serviceUsers = new ServiceUsers(_dbContext);  
        }

        [Fact]
        public void Add_ShouldAddUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", SecondName = "Smith", Age = 30 };
            user.FullName = $"{user.Name} {user.SecondName}";
            // Act
            _serviceUsers.Add(user);

            // Assert
            var addedUser = _dbContext.Users.Find(user.Id);
            Assert.NotNull(addedUser);
            Assert.Equal(user.Name, addedUser.Name);
            Assert.Equal(user.SecondName, addedUser.SecondName);
            Assert.Equal(user.Age, addedUser.Age);
        }

        [Fact]
        public void AddProfession_ShouldAddProfession()
        {
            // Arrange
            var professionName = "Engineer";

            // Act
            _serviceUsers.AddProfession(professionName);

            // Assert
            var addedProfession = _dbContext.Professions.FirstOrDefault(p => p.Name == professionName);
            Assert.NotNull(addedProfession);
            Assert.Equal(professionName, addedProfession.Name);
        }

        [Fact]
        public void EditProfessionUser_ShouldUpdateUserProfession()
        {
            // Arrange
            var profession = new Profession { Id = 1, Name = "Engineer" };
            _dbContext.Professions.Add(profession);
            var user = new User { Id = 1, Name = "John", SecondName = "Smith", ProfessionId = null };
            user.FullName = $"{user.Name} {user.SecondName}";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            _serviceUsers.EditProfessionUser(user.Id, profession.Id);

            // Assert
            var updatedUser = _dbContext.Users.Find(user.Id);
            Assert.NotNull(updatedUser);
            Assert.Equal(profession.Id, updatedUser.ProfessionId);
        }

        [Fact]
        public void EditName_ShouldUpdateUserName()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", SecondName = "Smith", Age = 30 };
            user.FullName = $"{user.Name} {user.SecondName}";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            _serviceUsers.EditName(1, "Mike");

            // Assert
            var updatedUser = _dbContext.Users.Find(1);
            Assert.NotNull(updatedUser);
            Assert.Equal("Mike", updatedUser.Name);
        }

        [Fact]
        public void EditAge_ShouldUpdateUserAge()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", SecondName = "Smith", Age = 30 };
            user.FullName = $"{user.Name} {user.SecondName}";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            _serviceUsers.EditAge(1, 35);

            // Assert
            var updatedUser = _dbContext.Users.Find(1);
            Assert.NotNull(updatedUser);
            Assert.Equal(35, updatedUser.Age);
        }
        [Fact]
        public void EditWages_ShouldUpdateUserWages()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", SecondName = "Smith", Age = 30, Wages = 1000 };
            user.FullName = $"{user.Name} {user.SecondName}";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            _serviceUsers.EditWages(1, 1200);

            // Assert
            var updatedUser = _dbContext.Users.Find(1);
            Assert.NotNull(updatedUser);
            Assert.Equal(1200, updatedUser.Wages);
        }

        [Fact]
        public void EditDateOfBirth_ShouldUpdateUserDateOfBirth()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", SecondName = "Smith", Age = 30, DateOfBirth = new DateTime(1991, 5, 1) };
            user.FullName = $"{user.Name} {user.SecondName}";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            _serviceUsers.EditDateOfBirth(1, new DateTime(1990, 5, 1));

            // Assert
            var updatedUser = _dbContext.Users.Find(1);
            Assert.NotNull(updatedUser);
            Assert.Equal(new DateTime(1990, 5, 1), updatedUser.DateOfBirth);
        }
        [Fact]
        public void Delete_ShouldRemoveUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", SecondName = "Smith", Age = 30 };
            user.FullName = $"{user.Name} {user.SecondName}";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            _serviceUsers.Delete(1);

            // Assert
            var deletedUser = _dbContext.Users.Find(1);
            Assert.Null(deletedUser);
        }

        [Fact]
        public void DeleteProfession_ShouldRemoveProfession()
        {
            // Arrange
            var profession = new Profession { Id = 1, Name = "Engineer" };
            _dbContext.Professions.Add(profession);
            _dbContext.SaveChanges();

            // Act
            _serviceUsers.DeleteProfession(profession.Id);

            // Assert
            var deletedProfession = _dbContext.Professions.Find(profession.Id);
            Assert.Null(deletedProfession);
        }

        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "John", SecondName = "Doe", Age = 30, FullName = "John Doe" },
                new User { Id = 2, Name = "Jane", SecondName = "Smith", Age = 25, FullName = "Jane Smith" }
            };
            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();

            // Act
            var allUsers = _serviceUsers.GetAllUsers();

            // Assert
            Assert.Equal(2, allUsers.Count);
            Assert.Contains(allUsers, u => u.Name == "John");
            Assert.Contains(allUsers, u => u.Name == "Jane");
        }

        [Fact]
        public void GetAllProfessionsUsers_ShouldReturnUserWithProfession()
        {
            // Arrange
            var profession = new Profession { Id = 1, Name = "Engineer" };
            _dbContext.Professions.Add(profession);
            var user = new User { Id = 1, Name = "John", SecondName = "Smith", ProfessionId = profession.Id, Profession = profession };
            user.FullName = $"{user.Name} {user.SecondName}";
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            var result = _serviceUsers.GetAllProfessionsUsers();

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result[0].UserName);
            Assert.Equal("Engineer", result[0].ProfessionName);
        }

        [Fact]
        public void GetAllProfessionsStats_ShouldReturnProfessionStats()
        {
            // Arrange
            var profession = new Profession { Id = 1, Name = "Engineer" };
            var user = new User { Id = 1, Name = "John", SecondName = "Smith", ProfessionId = profession.Id, Profession = profession };
            user.FullName = $"{user.Name} {user.SecondName}";
            _dbContext.Professions.Add(profession);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            var result = _serviceUsers.GetAllProfessionsStats();

            // Assert
            Assert.Single(result);
            Assert.Equal("Engineer", result[0].Name);
            Assert.Equal(1, result[0].Count);
        }

        [Fact]
        public void GetAllShortUsers_ShouldReturnPaginatedShortUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "John",SecondName = "Doe", DateOfBirth = new DateTime(1990, 1, 1),FullName = "John Doe" },
                new User { Id = 2, Name = "Jane",SecondName = "Smith", DateOfBirth = new DateTime(1992, 1, 1),FullName = "Jane Smith" },
                new User { Id = 3, Name = "Mike",SecondName = "Tyson", DateOfBirth = new DateTime(1995, 1, 1),FullName = "Mike Tyson" }
            };
            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();

            // Act
            var result = _serviceUsers.GetAllShortUsers(0, 2);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.Name == "John");
            Assert.Contains(result, u => u.Name == "Jane");
        }

        [Fact]
        public void SearchUsersMoreAge_ShouldReturnUsersAboveAge()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "John", SecondName = "Doe", Age = 30, FullName = "John Doe" },
                new User { Id = 2, Name = "Jane", SecondName = "Smith", Age = 25, FullName = "Jane Smith" }
            };
            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();

            // Act
            var result = _serviceUsers.SearchUsersMoreAge(26);

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }

        [Fact]
        public void SearchUsers_ShouldReturnUsersMatchingTerm()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "John", SecondName = "Doe", Age = 30, FullName = "John Doe" },
                new User { Id = 2, Name = "Jane", SecondName = "Smith", Age = 25, FullName = "Jane Smith" },
                new User { Id = 3, Name = "Johnny",SecondName = "Smith", Age = 35, FullName = "Johnny Smith"}
            };
            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();

            // Act
            var result = _serviceUsers.SearchUsers("John");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.Name == "John");
            Assert.Contains(result, u => u.Name == "Johnny");
        }
    }
}