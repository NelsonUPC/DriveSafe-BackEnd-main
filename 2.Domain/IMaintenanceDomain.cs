using _3.Data.Models;

namespace _2.Domain;

public interface IMaintenanceDomain
{
    Task<int> SaveAsync(Maintenance data);
}