using _3.Data.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using _3.Data.Context;

namespace _3.Data.Test
{
    public class VehicleDataUnitTest
    {
        Vehicle vehicle = new Vehicle()
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
        private Mock<DriveSafeDBContext> _dbContextMock;
        private Mock<DbSet<Vehicle>> _dbSetMock;
        private VehicleData _vehicleData;
        public VehicleDataUnitTest()
        {
            _dbContextMock = new Mock<DriveSafeDBContext>();
            _dbSetMock = new Mock<DbSet<Vehicle>>();
            _dbContextMock.Setup(x => x.Vehicles).Returns(_dbSetMock.Object);
            _vehicleData = new VehicleData(_dbContextMock.Object);
        }

        [Fact]
        public async Task SaveAsync_SavesVehicle()
        {
            //Arrange
            await _vehicleData.SaveAsync(vehicle);
            //Act
            _dbSetMock.Verify(x => x.Add(It.Is<Vehicle>(v => v == vehicle)), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
        
        [Fact]
        public async Task GetAllAsync_GetsAllActiveVehicles()
        {
            //Arrange
            var vehicles = new List<Vehicle> { vehicle, vehicle }.AsQueryable();
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(vehicles.Provider);
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(vehicles.Expression);
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(vehicles.ElementType);
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(vehicles.GetEnumerator());
            //Act
            var result = await _vehicleData.GetAllAsync();
            //Assert
            Assert.Equal(2, result.Count);
        }
        
        [Fact]
        public async Task GetByIdAsync_GetsVehicleById()
        {
            //Arrange
            var vehicles = new List<Vehicle> { vehicle }.AsQueryable();
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(vehicles.Provider);
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(vehicles.Expression);
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(vehicles.ElementType);
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(vehicles.GetEnumerator());
            //Act
            var result = await _vehicleData.GetByIdAsync(1);
            //Assert
            Assert.Equal(1, result.id);
        }
        
        [Fact]
        public async Task GetByUserIdAsync_GetsVehiclesByUserId()
        {
            //Arrange
            var vehicles = new List<Vehicle> { vehicle, vehicle }.AsQueryable();
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(vehicles.Provider);
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(vehicles.Expression);
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(vehicles.ElementType);
            _dbSetMock.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(vehicles.GetEnumerator());
            //Act
            var result = await _vehicleData.GetByUserIdAsync(1);
            //Assert
            Assert.Equal(2, result.Count);
        }
        
        [Fact]
        public async Task DeleteAsync_DeletesVehicle()
        {
            //Arrange
            _dbSetMock.Setup(x => x.FindAsync(1)).ReturnsAsync(vehicle);
        
            await _vehicleData.DeleteAsync(1);
            
            //Act
            _dbSetMock.Verify(x => x.Remove(It.Is<Vehicle>(v => v == vehicle)), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}