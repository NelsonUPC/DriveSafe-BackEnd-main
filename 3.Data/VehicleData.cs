using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data;

public class VehicleData : IVehicleData
{
    private DriveSafeDBContext _driveSafeDbcontext;
    
    public VehicleData (DriveSafeDBContext driveSafeDbContext)
    {
        _driveSafeDbcontext = driveSafeDbContext;
    }
    public async Task<int> SaveAsync(Vehicle data)
    {
        data.IsActive = true;
        using (var transaction = await _driveSafeDbcontext.Database.BeginTransactionAsync())
        {
            try
            {
                _driveSafeDbcontext.Vehicles.Add(data);
                await _driveSafeDbcontext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        return data.Id;
    }

    public async Task<List<Vehicle>> getAllAsync()
    {
        return await _driveSafeDbcontext.Vehicles.Where(v => v.IsActive).ToListAsync();
    }
}