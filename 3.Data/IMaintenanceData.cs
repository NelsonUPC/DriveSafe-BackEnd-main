using _3.Data.Models;

namespace _3.Data;

public interface IMaintenanceData
{
    Task<int> SaveAsync(Maintenance data);
    Task<List<Maintenance>> GetAllAsync();
    Task<Maintenance> GetByIdAsync(int id);
    Task<Boolean> UpdateAsync(Maintenance data, int id);
    Task<Boolean> DeleteAsync(int id);
}