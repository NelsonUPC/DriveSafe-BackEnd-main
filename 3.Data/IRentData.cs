using _3.Data.Models;

namespace _3.Data;

public interface IRentData
{
    Task<int> SaveAsync(Rent data);
    Task<List<Rent>> GetAllAsync();
    Task<Rent> GetByIdAsync(int id);
    Task<Rent> GetByUserIdAsync(int id);
    Task<Boolean> UpdateAsync(Rent data, int id);
    Task<Boolean> DeleteAsync(int id);
}