using _3.Data.Models;

namespace _2.Domain;

public interface IRentDomain
{
    Task<int> SaveAsync(Rent data);
}