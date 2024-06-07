using _1.API.Controllers;
using _1.API.Request;
using _2.Domain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace _1.API.Test;

public class VehicleControllerUnitTest
{
    [Fact]
    public async Task GetAsync_ReturnsOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var vehicleDataMock = Substitute.For<IVehicleData>();
        var vehicleDomainMock = Substitute.For<IVehicleDomain>();
        var vehicleController = new VehicleController(vehicleDataMock, vehicleDomainMock, mapperMock);

        var fakeVehicles = new List<Vehicle>
        {
            new Vehicle
            {
                brand = "Toyota",
                model = "Corolla",
                maximum_speed = 180,
                consumption = 15,
                dimensions = "4620mm x 1775mm x 1475mm",
                weight = 1320,
                car_class = "Compact",
                transmission = "Automatic",
                time_type = "Day",
                rental_cost = 50,
                pick_up_place = "Location 1",
                url_image = "https://example.com/image1.jpg",
                rent_status = "Available",
                owner_id = 1
            }
        };

        vehicleDataMock.GetAllAsync().Returns(fakeVehicles);

        // Act
        var result = await vehicleController.GetAsync();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAsyncById_ReturnsOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var vehicleDataMock = Substitute.For<IVehicleData>();
        var vehicleDomainMock = Substitute.For<IVehicleDomain>();
        var vehicleController = new VehicleController(vehicleDataMock, vehicleDomainMock, mapperMock);

        var fakeVehicle = new Vehicle
        {
            brand = "Toyota",
            model = "Corolla",
            maximum_speed = 180,
            consumption = 15,
            dimensions = "4620mm x 1775mm x 1475mm",
            weight = 1320,
            car_class = "Compact",
            transmission = "Automatic",
            time_type = "Day",
            rental_cost = 50,
            pick_up_place = "Location 1",
            url_image = "https://example.com/image1.jpg",
            rent_status = "Available",
            owner_id = 1
        };

        vehicleDataMock.GetByIdAsync(1).Returns(fakeVehicle);

        // Act
        var result = await vehicleController.GetAsyncById(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetByUserIdAsync_ReturnsOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var vehicleDataMock = Substitute.For<IVehicleData>();
        var vehicleDomainMock = Substitute.For<IVehicleDomain>();
        var vehicleController = new VehicleController(vehicleDataMock, vehicleDomainMock, mapperMock);

        var fakeVehicles = new List<Vehicle>
        {
            new Vehicle
            {
                brand = "Toyota",
                model = "Corolla",
                maximum_speed = 180,
                consumption = 15,
                dimensions = "4620mm x 1775mm x 1475mm",
                weight = 1320,
                car_class = "Compact",
                transmission = "Automatic",
                time_type = "Day",
                rental_cost = 50,
                pick_up_place = "Location 1",
                url_image = "https://example.com/image1.jpg",
                rent_status = "Available",
                owner_id = 1
            }
        };

        vehicleDataMock.GetByUserIdAsync(1).Returns(fakeVehicles);

        // Act
        var result = await vehicleController.GetByUserIdAsync(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task PostAsync_ReturnsCreated()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var vehicleDataMock = Substitute.For<IVehicleData>();
        var vehicleDomainMock = Substitute.For<IVehicleDomain>();
        var vehicleController = new VehicleController(vehicleDataMock, vehicleDomainMock, mapperMock);

        var fakeVehicleRequest = new VehicleRequest {
            brand = "Toyota",
            model = "Corolla",
            maximum_speed = 180,
            consumption = 15,
            dimensions = "4620mm x 1775mm x 1475mm",
            weight = 1320,
            car_class = "Compact",
            transmission = "Automatic",
            time_type = "Day",
            rental_cost = 50,
            pick_up_place = "Location 1",
            url_image = "https://example.com/image1.jpg",
            rent_status = "Available",
            owner_id = 1
        };
        var fakeVehicle = new Vehicle
        {
            brand = "Toyota",
            model = "Corolla",
            maximum_speed = 180,
            consumption = 15,
            dimensions = "4620mm x 1775mm x 1475mm",
            weight = 1320,
            car_class = "Compact",
            transmission = "Automatic",
            time_type = "Day",
            rental_cost = 50,
            pick_up_place = "Location 1",
            url_image = "https://example.com/image1.jpg",
            rent_status = "Available",
            owner_id = 1
        };

        mapperMock.Map<VehicleRequest, Vehicle>(fakeVehicleRequest).Returns(fakeVehicle);
        vehicleDomainMock.SaveAsync(fakeVehicle).Returns(1);

        // Act
        var result = await vehicleController.PostAsync(fakeVehicleRequest);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal("api/Vehicle", createdResult.Location);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsOk()
    {
        // Arrange
        var vehicleDataMock = Substitute.For<IVehicleData>();
        var vehicleDomainMock = Substitute.For<IVehicleDomain>();
        var vehicleController = new VehicleController(vehicleDataMock, vehicleDomainMock, null);

        vehicleDomainMock.DeleteAsync(1).Returns(true);

        // Act
        var result = await vehicleController.DeleteAsync(1);

        // Assert
        Assert.IsType<OkResult>(result);
    }
}