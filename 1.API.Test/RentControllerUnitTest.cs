using _1.API.Controllers;
using _1.API.Request;
using _2.Domain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace _1.API.Test;

public class RentControllerUnitTest
{
    private readonly RentRequest fakeRentRequest;
    private readonly List<Rent> fakeRents;

    public RentControllerUnitTest()
    {
        fakeRentRequest = new RentRequest
        {
            status = "Available",
            start_date = DateOnly.MinValue,
            end_date = DateOnly.MaxValue,
            vehicle_id = 1,
            owner_id = 1,
            tenant_id = 2,
            pick_up_place = "Location 1"
        };

        fakeRents = new List<Rent>
        {
            new Rent
            {
                status = "Available",
                start_date = DateOnly.MinValue,
                end_date = DateOnly.MaxValue,
                vehicle_id = 1,
                owner_id = 1,
                tenant_id = 2,
                pick_up_place = "Location 1"
            }
        };
    }

    [Fact]
    public async Task GetAsync_ReturnsOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var rentDataMock = Substitute.For<IRentData>();
        var rentDomainMock = Substitute.For<IRentDomain>();
        var rentController = new RentController(rentDataMock, rentDomainMock, mapperMock);

        rentDataMock.GetAllAsync().Returns(fakeRents);

        // Act
        var result = await rentController.GetAsync();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAsyncById_ReturnsOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var rentDataMock = Substitute.For<IRentData>();
        var rentDomainMock = Substitute.For<IRentDomain>();
        var rentController = new RentController(rentDataMock, rentDomainMock, mapperMock);

        rentDataMock.GetByIdAsync(1).Returns(fakeRents[0]);

        // Act
        var result = await rentController.GetAsyncById(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetByUserIdAsync_ReturnsOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var rentDataMock = Substitute.For<IRentData>();
        var rentDomainMock = Substitute.For<IRentDomain>();
        var rentController = new RentController(rentDataMock, rentDomainMock, mapperMock);

        rentDataMock.GetByUserIdAsync(1).Returns(fakeRents);

        // Act
        var result = await rentController.GetByUserIdAsync(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task PostAsync_ReturnsCreated()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var rentDataMock = Substitute.For<IRentData>();
        var rentDomainMock = Substitute.For<IRentDomain>();
        var rentController = new RentController(rentDataMock, rentDomainMock, mapperMock);

        var fakeRent = new Rent
        {
            status = "Available",
            start_date = DateOnly.MinValue,
            end_date = DateOnly.MaxValue,
            vehicle_id = 1,
            owner_id = 1,
            tenant_id = 2,
            pick_up_place = "Location 1"
        };
        
        mapperMock.Map<RentRequest, Rent>(fakeRentRequest).Returns(fakeRent);
        rentDomainMock.SaveAsync(fakeRent).Returns(1);

        // Act
        var result = await rentController.PostAsync(fakeRentRequest);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal("api/Rent", createdResult.Location);
    }

    [Fact]
    public async Task PutAsync_ReturnsOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var rentDataMock = Substitute.For<IRentData>();
        var rentDomainMock = Substitute.For<IRentDomain>();
        var rentController = new RentController(rentDataMock, rentDomainMock, mapperMock);

        var fakeRent = new Rent
        {
            status = "Available",
            start_date = DateOnly.MinValue,
            end_date = DateOnly.MaxValue,
            vehicle_id = 1,
            owner_id = 1,
            tenant_id = 2,
            pick_up_place = "Location 1"
        };

        mapperMock.Map<RentRequest, Rent>(fakeRentRequest).Returns(fakeRent);
        rentDomainMock.UpdateAsync(fakeRent, 1).Returns(true);

        // Act
        var result = await rentController.PutAsync(1, fakeRentRequest);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsOk()
    {
        // Arrange
        var rentDataMock = Substitute.For<IRentData>();
        var rentDomainMock = Substitute.For<IRentDomain>();
        var rentController = new RentController(rentDataMock, rentDomainMock, null);

        rentDomainMock.DeleteAsync(1).Returns(true);

        // Act
        var result = await rentController.DeleteAsync(1);

        // Assert
        Assert.IsType<OkResult>(result);
    }
}