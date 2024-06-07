using _3.Data;
using _3.Data.Models;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using _3.Data.Context;

namespace _3.Data.Test
{
    public class MaintenanceDataUnitTest
    {
        private Mock<DriveSafeDBContext> _dbContextMock;
        private Mock<DbSet<Maintenance>> _dbSetMock;
        private MaintenanceData _maintenanceData;
        private Maintenance _maintenance;

        public MaintenanceDataUnitTest()
        {
            _dbContextMock = new Mock<DriveSafeDBContext>();
            _dbSetMock = new Mock<DbSet<Maintenance>>();

            _dbContextMock.Setup(x => x.Maintenances).Returns(_dbSetMock.Object);

            _maintenanceData = new MaintenanceData(_dbContextMock.Object);

            _maintenance = new Maintenance()
            {
                type_problem = "problem",
                title = "title",
                description = "description",
                tenant_id = 1,
                owner_id = 2
            };
        }

        [Fact]
        public async Task SaveAsync_SavesMaintenance()
        {
            //Arrange
            await _maintenanceData.SaveAsync(_maintenance);
            
            //Act
            _dbSetMock.Verify(x => x.Add(It.Is<Maintenance>(m => m == _maintenance)), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_GetsAllActiveMaintenances()
        {
            //Assert
            var maintenances = new List<Maintenance> { _maintenance, _maintenance }.AsQueryable();

            _dbSetMock.As<IQueryable<Maintenance>>().Setup(m => m.Provider).Returns(maintenances.Provider);
            _dbSetMock.As<IQueryable<Maintenance>>().Setup(m => m.Expression).Returns(maintenances.Expression);
            _dbSetMock.As<IQueryable<Maintenance>>().Setup(m => m.ElementType).Returns(maintenances.ElementType);
            _dbSetMock.As<IQueryable<Maintenance>>().Setup(m => m.GetEnumerator()).Returns(maintenances.GetEnumerator());
            
            //Act
            var result = await _maintenanceData.GetAllAsync();
            
            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_GetsMaintenanceById()
        {
            //Arrange
            var maintenances = new List<Maintenance> { _maintenance }.AsQueryable();

            _dbSetMock.As<IQueryable<Maintenance>>().Setup(m => m.Provider).Returns(maintenances.Provider);
            _dbSetMock.As<IQueryable<Maintenance>>().Setup(m => m.Expression).Returns(maintenances.Expression);
            _dbSetMock.As<IQueryable<Maintenance>>().Setup(m => m.ElementType).Returns(maintenances.ElementType);
            _dbSetMock.As<IQueryable<Maintenance>>().Setup(m => m.GetEnumerator()).Returns(maintenances.GetEnumerator());
            
            //Act
            
            var result = await _maintenanceData.GetByIdAsync(1);
            
            //Assert
            Assert.Equal(1, result.id);
        }
    }
}