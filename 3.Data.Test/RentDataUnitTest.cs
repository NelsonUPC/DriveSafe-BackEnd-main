using _3.Data;
using _3.Data.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using _3.Data.Context;

namespace _3.Data.Test
{
    public class RentDataUnitTest
    {
        private Mock<DriveSafeDBContext> _dbContextMock;
        private Mock<DbSet<Rent>> _dbSetMock;
        private RentData _rentData;
        private Rent _rent;

        public RentDataUnitTest()
        {
            _dbContextMock = new Mock<DriveSafeDBContext>();
            _dbSetMock = new Mock<DbSet<Rent>>();

            _dbContextMock.Setup(x => x.Rents).Returns(_dbSetMock.Object);

            _rentData = new RentData(_dbContextMock.Object);

            _rent = new Rent()
            {
                status = "status",
                start_date = DateOnly.MinValue,
                end_date = DateOnly.MaxValue,
                vehicle_id = 1,
                owner_id = 1,
                tenant_id = 1,
                pick_up_place = "place"
            };
        }

        [Fact]
        public async Task SaveAsync_SavesRent()
        {
            // Arrange
            _dbSetMock.Setup(x => x.Add(It.Is<Rent>(r => r == _rent)));
            _dbContextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _rentData.SaveAsync(_rent);

            // Assert
            _dbSetMock.Verify(x => x.Add(It.Is<Rent>(r => r == _rent)), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAllAsync_GetsAllActiveRents()
        {
            //Arrange
            var rents = new List<Rent> { _rent, _rent }.AsQueryable();

            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.Provider).Returns(rents.Provider);
            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.Expression).Returns(rents.Expression);
            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.ElementType).Returns(rents.ElementType);
            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.GetEnumerator()).Returns(rents.GetEnumerator());
            
            //Act
            var result = await _rentData.GetAllAsync();
            
            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_GetsRentById()
        {
            //Arrange
            var rents = new List<Rent> { _rent }.AsQueryable();

            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.Provider).Returns(rents.Provider);
            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.Expression).Returns(rents.Expression);
            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.ElementType).Returns(rents.ElementType);
            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.GetEnumerator()).Returns(rents.GetEnumerator());
            
            //Act
            var result = await _rentData.GetByIdAsync(1);
            
            //Assert
            Assert.Equal(1, result.id);
        }

        [Fact]
        public async Task GetByUserIdAsync_GetsRentsByUserId()
        {
            //Arrange
            var rents = new List<Rent> { _rent, _rent }.AsQueryable();

            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.Provider).Returns(rents.Provider);
            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.Expression).Returns(rents.Expression);
            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.ElementType).Returns(rents.ElementType);
            _dbSetMock.As<IQueryable<Rent>>().Setup(m => m.GetEnumerator()).Returns(rents.GetEnumerator());
            
            //Act
            var result = await _rentData.GetByUserIdAsync(1);
            
            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesRent()
        {
            //Arrange
            _dbSetMock.Setup(x => x.FindAsync(1)).ReturnsAsync(_rent);
            
            //Act
            await _rentData.UpdateAsync(_rent, 1);
            
            //Assert
            _dbSetMock.Verify(x => x.Update(It.Is<Rent>(r => r == _rent)), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeletesRent()
        {
            //Arrange
            _dbSetMock.Setup(x => x.FindAsync(1)).ReturnsAsync(_rent);
            
            //Act
            await _rentData.DeleteAsync(1);
            
            //Assert
            _dbSetMock.Verify(x => x.Remove(It.Is<Rent>(r => r == _rent)), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}