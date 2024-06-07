using _3.Data;
using _3.Data.Models;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using _3.Data.Context;

namespace _3.Data.Test
{
    public class UserDataUnitTest
    {
        private Mock<DriveSafeDBContext> _dbContextMock;
        private Mock<DbSet<User>> _dbSetMock;
        private UserData _userData;
        private User _user;

        public UserDataUnitTest()
        {
            _dbContextMock = new Mock<DriveSafeDBContext>();
            _dbSetMock = new Mock<DbSet<User>>();

            _dbContextMock.Setup(x => x.Users).Returns(_dbSetMock.Object);

            _userData = new UserData(_dbContextMock.Object);

            _user = new User()
            {
                name = "John",
                last_name = "Doe",
                birthdate = DateOnly.MinValue,
                cellphone = 123456789,
                gmail = "john.doe@gmail.com",
                password = "password",
                type = "type"
            };
        }

        [Fact]
        public async Task SaveAsync_SavesUser()
        {
            
            // Arrange
            await _userData.SaveAsync(_user);
            
            // Act
            _dbSetMock.Verify(x => x.Add(It.Is<User>(u => u == _user)), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_GetsAllActiveUsers()
        {
            //Arrange
            var users = new List<User> { _user, _user }.AsQueryable();

            _dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            _dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            _dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            _dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            
            //Act
            var result = await _userData.GetAllAsync();
            
            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_GetsUserById()
        {
            //Arrange
            var users = new List<User> { _user }.AsQueryable();

            _dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            _dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            _dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            _dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            
            //Act
            var result = await _userData.GetByIdAsync(1);
            
            //Assert
            Assert.Equal(1, result.id);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesUser()
        {
            //Arrange
            _dbSetMock.Setup(x => x.FindAsync(1)).ReturnsAsync(_user);
            
            //Act
            await _userData.UpdateAsync(_user, 1);

            _dbSetMock.Verify(x => x.Update(It.Is<User>(u => u == _user)), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeletesUser()
        {
            //Arrange
            _dbSetMock.Setup(x => x.FindAsync(1)).ReturnsAsync(_user);
            
            //Act
            await _userData.DeleteAsync(1);

            _dbSetMock.Verify(x => x.Remove(It.Is<User>(u => u == _user)), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task IsEmailInUseAsync_ChecksIfEmailIsInUse()
        {
            //Arrange
            var users = new List<User> { _user }.AsQueryable();

            _dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            _dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            _dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            _dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            
            //Act
            var result = await _userData.IsEmailInUseAsync("john.doe@gmail.com");
            
            //Assert
            Assert.True(result);
        }
    }
}