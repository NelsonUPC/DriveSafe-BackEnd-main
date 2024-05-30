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
        return data.id;
    }
    public async Task<List<Vehicle>> GetAllAsync()
    {
        return await _driveSafeDbcontext.Vehicles.Where(v => v.IsActive).ToListAsync();
    }
    public async Task<Vehicle> GetByIdAsync(int id)
    {
        return await _driveSafeDbcontext.Vehicles.Where(v => v.id == id).FirstOrDefaultAsync();
    }

    public async Task<Vehicle> GetByUserIdAsync(int user_id)
    {
        return await _driveSafeDbcontext.Vehicles.FirstOrDefaultAsync(v => v.owner_id == user_id && v.IsActive == true);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using (var transaction = await _driveSafeDbcontext.Database.BeginTransactionAsync())
        {
            var vehicleToDelete = await _driveSafeDbcontext.Vehicles.FirstOrDefaultAsync(v => v.id == id);
            if (vehicleToDelete != null)
            {
                _driveSafeDbcontext.Vehicles.Remove(vehicleToDelete);
                await _driveSafeDbcontext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        return true;
    }
}