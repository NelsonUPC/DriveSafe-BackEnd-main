using _3.Data.Models;

namespace _3.Data;

public interface IVehicleData
{
    Task<int> SaveAsync(Vehicle data);
    Task<List<Vehicle>> GetAllAsync();
    Task<Vehicle> GetByIdAsync(int id);
    Task<List<Vehicle>> GetByUserIdAsync(int user_id);
    Task<Boolean> DeleteAsync(int id);
}