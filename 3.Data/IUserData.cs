using _3.Data.Models;

namespace _3.Data;

public interface IUserData
{
    Task<int> SaveAsync(User data);
    Task<List<User>> getAllAsync();
}