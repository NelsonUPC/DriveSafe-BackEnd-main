using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data;

public class UsuarioData : IUsuarioData
{
    private DriveSafeDBContext _driveSafeDbcontext;
    public UsuarioData(DriveSafeDBContext driveSafeDbContext)
    {
        _driveSafeDbcontext = driveSafeDbContext;
    }
    public async Task<int> SaveAsync(Usuario data)
    {
        data.IsActive = true;

        using (var transaction = await _driveSafeDbcontext.Database.BeginTransactionAsync())
        {
            try
            {
                _driveSafeDbcontext.Usuarios.Add(data);
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

    public async Task<List<Usuario>> getAllAsync()
    {
        return await _driveSafeDbcontext.Usuarios.Where(u => u.IsActive).ToListAsync();
    }
}