using _3.Data;
using _3.Data.Models;
using Moq;
using Xunit;

namespace _2.Domain.Test;

public class RentDomainUnitTest
{
    private Mock<IRentData> _rentDataMock;
    private RentDomain _rentDomain;
    private Rent _rent;

    public RentDomainUnitTest()
    {
        _rentDataMock = new Mock<IRentData>();
        _rentDomain = new RentDomain(_rentDataMock.Object);

        _rent = new Rent()
        {
            status = "Pending",
            start_date = DateOnly.Parse("2022-04-30"),
            end_date = DateOnly.Parse("2022-05-30"),
            vehicle_id = 1,
            owner_id = 1,
            tenant_id = 2,
            pick_up_place = "Bandung"
        };
    }

    [Fact]
    public async Task SaveAsync_ValidRent_ReturnsValidId()
    {
        // Arrange
        _rentDataMock.Setup(m => m.SaveAsync(_rent)).ReturnsAsync(1);

        // Act
        var result = await _rentDomain.SaveAsync(_rent);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_ValidRent_ReturnsTrue()
    {
        // Arrange
        _rentDataMock.Setup(m => m.UpdateAsync(_rent, 1)).ReturnsAsync(true);

        // Act
        var result = await _rentDomain.UpdateAsync(_rent, 1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnsTrue()
    {
        // Arrange
        _rentDataMock.Setup(m => m.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _rentDomain.DeleteAsync(1);

        // Assert
        Assert.True(result);
    }
}