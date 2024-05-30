using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data;

public class RentData : IRentData
{
    private DriveSafeDBContext _driveSafeDbcontext;
    public RentData(DriveSafeDBContext driveSafeDbContext)
    {
        _driveSafeDbcontext = driveSafeDbContext;
    }
    public async Task<int> SaveAsync(Rent data)
    {
        data.IsActive = true;
        using (var transaction = await _driveSafeDbcontext.Database.BeginTransactionAsync())
        {
            try
            {
                _driveSafeDbcontext.Rents.Add(data);
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

    public async Task<List<Rent>> GetAllAsync()
    {
        return await _driveSafeDbcontext.Rents.Where(r => r.IsActive).ToListAsync();
    }

    public async Task<Rent> GetByIdAsync(int id)
    {
        return await _driveSafeDbcontext.Rents.Where(r => r.id == id).FirstOrDefaultAsync();
    }

    public Task<bool> UpdateAsync(Rent data, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}