using _3.Data;
using _3.Data.Models;

namespace _2.Domain;

public class RentDomain : IRentDomain
{
    private IRentData _rentData;
    public RentDomain(IRentData rentData)
    {
        _rentData = rentData;
    }
    public async Task<int> SaveAsync(Rent data)
    {
        return await _rentData.SaveAsync(data);
    }

    public async Task<Boolean> UpdateAsync(Rent data, int id)
    {
        return await _rentData.UpdateAsync(data, id);
    }

    public async Task<Boolean> DeleteAsync(int id)
    {
        return await _rentData.DeleteAsync(id);
    }
}