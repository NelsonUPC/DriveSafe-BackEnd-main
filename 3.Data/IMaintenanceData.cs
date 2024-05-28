using _3.Data.Models;

namespace _3.Data;

public interface IMaintenanceData
{
    Task<int> SaveAsync(Maintenance data);
    Task<List<Maintenance>> getAllAsync();
}