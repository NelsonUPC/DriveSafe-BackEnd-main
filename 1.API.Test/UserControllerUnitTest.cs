using _1.API.Controllers;
using _1.API.Request;
using _1.API.Response;
using _2.Domain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace _1.API.Test;

public class UserControllerUnitTest
{
    [Fact]
    public async Task GetAsync_ReturnSuccess()
    {
        //Arrange
        var mapperMock = Substitute.For<IMapper>();
        var userDataMock = Substitute.For<IUserData>();
        var userDomainMock = Substitute.For<IUserDomain>();
        var userController = new UserController(userDataMock, userDomainMock, mapperMock);

        var fakelist = new List<User>
        {
            new User
            {
                name = "John",
                last_name = "Doe",
                birthdate = DateOnly.Parse("1945-04-30"),
                cellphone = 1234567890,
                gmail = "johndoe@gmail.com",
                password = "123456",
                type = "tenant"
            }
        };

        var returnList = new List<UserResponse>
        {
            new UserResponse
            {
                name = "John",
                last_name = "Doe",
                birthdate = DateOnly.Parse("1945-04-30"),
                cellphone = 1234567890,
                gmail = "johndoe@gmail.com",
                password = "123456",
                type = "tenant"
            }
        };
        
        userDataMock.GetAllAsync().Returns(fakelist);
        mapperMock.Map<List<User>, List<UserResponse>>(fakelist).Returns(returnList);
        
        //Act
        var result = await userController.GetAsync();
        
        //Assert
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task GetAsync_ReturnNotFound()
    {
        //Arrange
        var mapperMock = Substitute.For<IMapper>();
        var userDataMock = Substitute.For<IUserData>();
        var userDomainMock = Substitute.For<IUserDomain>();
        var userController = new UserController(userDataMock, userDomainMock, mapperMock);

        var fakelist = new List<User>();
        var returnList = new List<UserResponse>();
        
        userDataMock.GetAllAsync().Returns(fakelist);
        mapperMock.Map<List<User>, List<UserResponse>>(fakelist).Returns(returnList);
        
        //Act
        var result = await userController.GetAsync();
        
        //Assert
        Assert.IsType<NotFoundResult>(result);
    }
    
    [Fact]
    public async Task GetAsyncById_ReturnSuccess()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var userDataMock = Substitute.For<IUserData>();
        var userDomainMock = Substitute.For<IUserDomain>();
        var userController = new UserController(userDataMock, userDomainMock, mapperMock);

        var fakeUser = new User
        {
            name = "John",
            last_name = "Doe",
            birthdate = DateOnly.Parse("1945-04-30"),
            cellphone = 1234567890,
            gmail = "johndoe@gmail.com",
            password = "123456",
            type = "tenant"
        };

        var returnUser = new UserResponse
        {
            name = "John",
            last_name = "Doe",
            birthdate = DateOnly.Parse("1945-04-30"),
            cellphone = 1234567890,
            gmail = "johndoe@gmail.com",
            password = "123456",
            type = "tenant"
        };

        userDataMock.GetByIdAsync(1).Returns(fakeUser);
        mapperMock.Map<User, UserResponse>(fakeUser).Returns(returnUser);

        // Act
        var result = await userController.GetAsyncById(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task PostAsync_ValidUser_ReturnsCreated()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var userDataMock = Substitute.For<IUserData>();
        var userDomainMock = Substitute.For<IUserDomain>();
        var userController = new UserController(userDataMock, userDomainMock, mapperMock);
    
        var fakeUserRequest = new UserRequest
        {
            name = "John",
            last_name = "Doe",
            birthdate = DateOnly.Parse("1945-04-30"),
            cellphone = 1234567890,
            gmail = "johndoe@gmail.com",
            password = "123456",
            type = "tenant"
        };
    
        var fakeUser = new User
        {
            name = "John",
            last_name = "Doe",
            birthdate = DateOnly.Parse("1945-04-30"),
            cellphone = 1234567890,
            gmail = "johndoe@gmail.com",
            password = "123456",
            type = "tenant"
        };
    
        mapperMock.Map<UserRequest, User>(fakeUserRequest).Returns(fakeUser);
        userDomainMock.SaveAsync(fakeUser).Returns(1);
    
        // Act
        var result = await userController.PostAsync(fakeUserRequest);
    
        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal("api/User", createdResult.Location);
    }
    
    [Fact]
    public async Task PutAsync_ValidUser_ReturnsOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var userDataMock = Substitute.For<IUserData>();
        var userDomainMock = Substitute.For<IUserDomain>();
        var userController = new UserController(userDataMock, userDomainMock, mapperMock);
    
        var fakeUserRequest = new UserRequest
        {
            name = "John",
            last_name = "Doe",
            birthdate = DateOnly.Parse("1945-04-30"),
            cellphone = 1234567890,
            gmail = "johndoe@gmail.com",
            password = "123456",
            type = "tenant"
        };
    
        var fakeUser = new User
        {
            name = "John",
            last_name = "Doe",
            birthdate = DateOnly.Parse("1945-04-30"),
            cellphone = 1234567890,
            gmail = "johndoe@gmail.com",
            password = "123456",
            type = "tenant"
        };
    
        mapperMock.Map<UserRequest, User>(fakeUserRequest).Returns(fakeUser);
        userDomainMock.UpdateAsync(fakeUser, 1).Returns(true);
    
        // Act
        var result = await userController.PutAsync(1, fakeUserRequest);
    
        // Assert
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeleteAsync_ValidId_ReturnsOk()
    {
        // Arrange
        var userDataMock = Substitute.For<IUserData>();
        var userDomainMock = Substitute.For<IUserDomain>();
        var userController = new UserController(userDataMock, userDomainMock, null);
    
        userDomainMock.DeleteAsync(1).Returns(true);
    
        // Act
        var result = await userController.DeleteAsync(1);
    
        // Assert
        Assert.IsType<OkResult>(result);
    }
}