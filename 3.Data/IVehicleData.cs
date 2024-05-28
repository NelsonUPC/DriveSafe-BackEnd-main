using _3.Data.Models;

namespace _3.Data;

public interface IVehicleData
{
    Task<int> SaveAsync(Vehicle data);
    Task<List<Vehicle>> getAllAsync();
}