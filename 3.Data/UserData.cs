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
    public async Task<List<User>> GetAllAsync()
    {
        return await _driveSafeDbcontext.Users.Where(u => u.IsActive).ToListAsync();
    }
    public async Task<User> GetByIdAsync(int id)
    {
        return await _driveSafeDbcontext.Users.Where(u => u.id == id).FirstOrDefaultAsync();
    }

    public async Task<Boolean> UpdateAsync(User data, int id)
    {
        using (var transaction = await _driveSafeDbcontext.Database.BeginTransactionAsync())
        {
            var userToUpdate = _driveSafeDbcontext.Users.Where(u => u.id == id).FirstOrDefault();
            userToUpdate.name = data.name;
            userToUpdate.last_name = data.last_name;
            userToUpdate.birthdate = data.birthdate;
            userToUpdate.cellphone = data.cellphone;
            userToUpdate.gmail = data.gmail;
            userToUpdate.password = data.password;
            
            _driveSafeDbcontext.Users.Update(userToUpdate);
            await _driveSafeDbcontext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        return true;
    }

    public async Task<Boolean> DeleteAsync(int id)
    {
        using (var transaction = await _driveSafeDbcontext.Database.BeginTransactionAsync())
        {
            var userToDelete = await _driveSafeDbcontext.Users.FirstOrDefaultAsync(u => u.id == id);
            if (userToDelete != null)
            {
                _driveSafeDbcontext.Users.Remove(userToDelete);
                await _driveSafeDbcontext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        return true;
    }
}