using _3.Data.Models;

namespace _3.Data;

public interface IUserData
{
    Task<int> SaveAsync(User data);
    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<Boolean> UpdateAsync(User data, int id);
    Task<Boolean> DeleteAsync(int id);
}