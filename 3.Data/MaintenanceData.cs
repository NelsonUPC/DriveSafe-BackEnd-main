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

    public async Task<List<Maintenance>> getAllAsync()
    {
        return await _driveSafeDbContext.Maintenances.Where(u => u.IsActive).ToListAsync();
    }
}