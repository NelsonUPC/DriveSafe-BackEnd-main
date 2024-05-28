using _3.Data;
using _3.Data.Models;

namespace _2.Domain;

public class UserDomain : IUserDomain
{
    private IUserData _userData;
    public UserDomain(IUserData userData)
    {
        _userData = userData;
    }
    public async Task<int> SaveAsync(User data)
    {
        return await _userData.SaveAsync(data);
    }
}