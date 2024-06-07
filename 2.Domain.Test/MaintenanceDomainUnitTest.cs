using _3.Data;
using _3.Data.Models;
using Moq;
namespace _2.Domain.Test;

public class MaintenanceDomainUnitTest
{
    private Mock<IMaintenanceData> _maintenanceDataMock;
    private MaintenanceDomain _maintenanceDomain;
    private Maintenance _maintenance;

    public MaintenanceDomainUnitTest()
    {
        _maintenanceDataMock = new Mock<IMaintenanceData>();
        _maintenanceDomain = new MaintenanceDomain(_maintenanceDataMock.Object);

        _maintenance = new Maintenance()
        {
            type_problem = "problem",
            title = "title",
            description = "description",
            tenant_id = 1,
            owner_id = 1
        };
    }

    [Fact]
    public async Task SaveAsync_ValidMaintenance_ReturnsValidId()
    {
        // Arrange
        _maintenanceDataMock.Setup(m => m.SaveAsync(_maintenance)).ReturnsAsync(1);

        // Act
        var result = await _maintenanceDomain.SaveAsync(_maintenance);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task SaveAsync_NullMaintenance_ThrowsArgumentNullException()
    {
        // Arrange
        Maintenance nullMaintenance = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _maintenanceDomain.SaveAsync(nullMaintenance));
    }

    [Fact]
    public async Task SaveAsync_InvalidMaintenance_ThrowsArgumentException()
    {
        // Arrange
        Maintenance invalidMaintenance = new Maintenance()
        {
            type_problem = "", // Invalid type_problem
            title = "title",
            description = "description",
            tenant_id = 1,
            owner_id = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _maintenanceDomain.SaveAsync(invalidMaintenance));
    }
}