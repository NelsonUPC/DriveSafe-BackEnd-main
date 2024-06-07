using _3.Data;
using _3.Data.Models;
using Moq;

namespace _2.Domain.Test;

public class UserDomainUnitTest
{
    private Mock<IUserData> _userDataMock;
    private UserDomain _userDomain;
    private User _user;

    public UserDomainUnitTest()
    {
        _userDataMock = new Mock<IUserData>();
        _userDomain = new UserDomain(_userDataMock.Object);

        _user = new User()
        {
            name = "John",
            last_name = "Doe",
            birthdate = DateOnly.Parse("1945-04-30"),
            cellphone = 1234567890,
            gmail = "johndoe@gmail.com",
            password = "123456",
            type = "tenant"
        };
    }

    [Fact]
    public async Task SaveAsync_ValidUser_ReturnsValidId()
    {
        // Arrange
        _userDataMock.Setup(m => m.IsEmailInUseAsync(_user.gmail)).ReturnsAsync(false);
        _userDataMock.Setup(m => m.SaveAsync(_user)).ReturnsAsync(1);

        // Act
        var result = await _userDomain.SaveAsync(_user);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task SaveAsync_EmailInUse_ThrowsException()
    {
        // Arrange
        _userDataMock.Setup(m => m.IsEmailInUseAsync(_user.gmail)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userDomain.SaveAsync(_user));
    }

    [Fact]
    public async Task UpdateAsync_ValidUser_ReturnsTrue()
    {
        // Arrange
        _userDataMock.Setup(m => m.GetByIdAsync(1)).ReturnsAsync(_user);
        _userDataMock.Setup(m => m.IsEmailInUseAsync(_user.gmail)).ReturnsAsync(false);
        _userDataMock.Setup(m => m.UpdateAsync(_user, 1)).ReturnsAsync(true);

        // Act
        var result = await _userDomain.UpdateAsync(_user, 1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_UserNotFound_ThrowsException()
    {
        // Arrange
        _userDataMock.Setup(m => m.GetByIdAsync(1)).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userDomain.UpdateAsync(_user, 1));
    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnsTrue()
    {
        // Arrange
        _userDataMock.Setup(m => m.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _userDomain.DeleteAsync(1);

        // Assert
        Assert.True(result);
    }
}