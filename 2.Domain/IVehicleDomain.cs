using _3.Data;
using _3.Data.Models;

namespace _2.Domain;

public interface IVehicleDomain
{
    Task<int> SaveAsync(Vehicle data);
}