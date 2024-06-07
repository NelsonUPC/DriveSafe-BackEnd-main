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
        if (await _userData.IsEmailInUseAsync(data.gmail))
        {
            throw new Exception("Email is already in use");
        }
        return await _userData.SaveAsync(data);
    }

    public async Task<Boolean> UpdateAsync(User data, int id)
    {
        var existingUser = await _userData.GetByIdAsync(id);
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }
        if (string.IsNullOrEmpty(data.name) || string .IsNullOrEmpty(data.last_name))
        {
            throw new Exception("Name and Last Name are required");
        }
        if (data.birthdate.Year < 1945)
        {
            throw new Exception("Birthdate cannot be before 1945");
        }
        existingUser.name = data.name;
        existingUser.last_name = data.last_name;
        existingUser.birthdate = data.birthdate;
        existingUser.cellphone = data.cellphone;
        existingUser.gmail = data.gmail;
        existingUser.password = data.password;
        existingUser.type = data.type;
        var result = await _userData.UpdateAsync(existingUser, id);
        return result;
    }

    public async Task<Boolean> DeleteAsync(int id)
    {
        return await _userData.DeleteAsync(id);
    }
}