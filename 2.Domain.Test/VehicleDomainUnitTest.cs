using _3.Data;
using _3.Data.Models;
using Moq;

namespace _2.Domain.Test;

public class VehicleDomainUnitTest
{
    private Mock<IVehicleData> _vehicleDataMock;
    private VehicleDomain _vehicleDomain;
    private Vehicle _vehicle;

    public VehicleDomainUnitTest()
    {
        _vehicleDataMock = new Mock<IVehicleData>();
        _vehicleDomain = new VehicleDomain(_vehicleDataMock.Object);

        _vehicle = new Vehicle()
        {
            brand = "Toyota",
            model = "Corolla",
            maximum_speed = 180,
            consumption = 10,
            dimensions = "200-175-146",
            weight = 1200,
            car_class = "Sedan",
            transmission = "Automatic",
            time_type = "Weekly",
            rental_cost = 100,
            pick_up_place = "Airport",
            url_image = "https://www.toyota.com/content/vehicle-landing/2022/corolla/site-specs/en/2022-corolla-specs-l-01.jpg",
            rent_status = "Available",
            owner_id = 1
        };
    }

    [Fact]
    public async Task SaveAsync_ValidVehicle_ReturnsValidId()
    {
        // Arrange
        _vehicleDataMock.Setup(m => m.SaveAsync(_vehicle)).ReturnsAsync(1);

        // Act
        var result = await _vehicleDomain.SaveAsync(_vehicle);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnsTrue()
    {
        // Arrange
        _vehicleDataMock.Setup(m => m.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _vehicleDomain.DeleteAsync(1);

        // Assert
        Assert.True(result);
    }
}