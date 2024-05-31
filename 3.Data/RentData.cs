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
    public async Task<List<Rent>> GetByUserIdAsync(int user_id)
    {
        return await _driveSafeDbcontext.Rents.Where(r => r.tenant_id == user_id && r.IsActive == true).ToListAsync();
    }
    public async Task<Boolean> UpdateAsync(Rent data, int id)
    {
        using (var transaction = await _driveSafeDbcontext.Database.BeginTransactionAsync())
        {
            var rentToUpdate = _driveSafeDbcontext.Rents.Where(r => r.id == id).FirstOrDefault();
            rentToUpdate.status = data.status;
            rentToUpdate.start_date = data.start_date;
            rentToUpdate.end_date = data.end_date;
            rentToUpdate.vehicle_id = data.vehicle_id;
            rentToUpdate.owner_id = data.owner_id;
            rentToUpdate.tenant_id = data.tenant_id;
            rentToUpdate.pick_up_place = data.pick_up_place;
            _driveSafeDbcontext.Rents.Update(rentToUpdate);
            await _driveSafeDbcontext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        return true;
    }
    public async Task<Boolean> DeleteAsync(int id)
    {
        using (var transaction = await _driveSafeDbcontext.Database.BeginTransactionAsync())
        {
            var rentToDelete = await _driveSafeDbcontext.Rents.FirstOrDefaultAsync(r => r.id == id);
            if (rentToDelete == null)
            {
                return false;
            }
            _driveSafeDbcontext.Rents.Remove(rentToDelete);
            await _driveSafeDbcontext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        return true;
    }
}