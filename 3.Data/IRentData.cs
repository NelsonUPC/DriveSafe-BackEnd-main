using _3.Data.Models;

namespace _3.Data;

public interface IRentData
{
    Task<int> SaveAsync(Rent data);
    Task<List<Rent>> getAllAsync();
}