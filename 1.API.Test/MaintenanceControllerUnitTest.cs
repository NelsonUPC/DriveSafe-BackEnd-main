using _1.API.Controllers;
using _1.API.Request;
using _2.Domain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace _1.API.Test;

public class MaintenanceControllerUnitTest
{
    private readonly MaintenanceRequest fakeMaintenanceRequest;
    private readonly List<Maintenance> fakeMaintenances;

    public MaintenanceControllerUnitTest()
    {
        fakeMaintenanceRequest = new MaintenanceRequest
        {
            type_problem = "Engine",
            title = "Engine Failure",
            description = "The engine is not starting",
            tenant_id = 1,
            owner_id = 2
        };

        fakeMaintenances = new List<Maintenance>
        {
            new Maintenance
            {
                type_problem = "Engine",
                title = "Engine Failure",
                description = "The engine is not starting",
                tenant_id = 1,
                owner_id = 2
            }
        };
    }

    [Fact]
    public async Task GetAsync_ReturnsOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var maintenanceDataMock = Substitute.For<IMaintenanceData>();
        var maintenanceDomainMock = Substitute.For<IMaintenanceDomain>();
        var maintenanceController = new MaintenanceController(maintenanceDataMock, maintenanceDomainMock, mapperMock);

        maintenanceDataMock.GetAllAsync().Returns(fakeMaintenances);

        // Act
        var result = await maintenanceController.GetAsync();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAsyncById_ReturnsOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var maintenanceDataMock = Substitute.For<IMaintenanceData>();
        var maintenanceDomainMock = Substitute.For<IMaintenanceDomain>();
        var maintenanceController = new MaintenanceController(maintenanceDataMock, maintenanceDomainMock, mapperMock);

        maintenanceDataMock.GetByIdAsync(1).Returns(fakeMaintenances[0]);

        // Act
        var result = await maintenanceController.GetAsyncById(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task PostAsync_ReturnsCreated()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var maintenanceDataMock = Substitute.For<IMaintenanceData>();
        var maintenanceDomainMock = Substitute.For<IMaintenanceDomain>();
        var maintenanceController = new MaintenanceController(maintenanceDataMock, maintenanceDomainMock, mapperMock);

        var fakeMaintenance = new Maintenance
        {
            type_problem = "Engine",
            title = "Engine Failure",
            description = "The engine is not starting",
            tenant_id = 1,
            owner_id = 2
        };

        mapperMock.Map<MaintenanceRequest, Maintenance>(fakeMaintenanceRequest).Returns(fakeMaintenance);
        maintenanceDomainMock.SaveAsync(fakeMaintenance).Returns(1);

        // Act
        var result = await maintenanceController.PostAsync(fakeMaintenanceRequest);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal("api/Maintenance", createdResult.Location);
    }
}