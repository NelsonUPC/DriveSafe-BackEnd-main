using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data;

public class MaintenanceData : IMaintenanceData
{
    private DriveSafeDBContext _driveSafeDbContext;
    public MaintenanceData(DriveSafeDBContext driveSafeDbContext)
    {
        _driveSafeDbContext = driveSafeDbContext;
    }
    public async Task<int> SaveAsync(Maintenance data)
    {
        data.IsActive = true;
        using (var transaction = await _driveSafeDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                _driveSafeDbContext.Maintenances.Add(data);
                await _driveSafeDbContext.SaveChangesAsync();
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

    public async Task<List<Maintenance>> GetAllAsync()
    {
        return await _driveSafeDbContext.Maintenances.Where(u => u.IsActive).ToListAsync();
    }
    
    public async Task<Maintenance> GetByIdAsync(int id)
    {
        return await _driveSafeDbContext.Maintenances.Where(m => m.id == id).FirstOrDefaultAsync();
    }

    public Task<bool> UpdateAsync(Maintenance data, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}