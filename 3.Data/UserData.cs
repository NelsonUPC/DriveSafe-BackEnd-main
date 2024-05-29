using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data;

public class UserData : IUserData
{
    private DriveSafeDBContext _driveSafeDbcontext;
    public UserData(DriveSafeDBContext driveSafeDbContext)
    {
        _driveSafeDbcontext = driveSafeDbContext;
    }
    public async Task<int> SaveAsync(User data)
    {
        data.IsActive = true;
        using (var transaction = await _driveSafeDbcontext.Database.BeginTransactionAsync())
        {
            try
            {
                _driveSafeDbcontext.Users.Add(data);
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

    public async Task<List<User>> getAllAsync()
    {
        return await _driveSafeDbcontext.Users.Where(u => u.IsActive).ToListAsync();
    }
}