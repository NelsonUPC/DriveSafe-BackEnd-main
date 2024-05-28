using _3.Data.Models;

namespace _2.Domain;

public interface IUsuarioDomain
{
    Task<int> SaveAsync(Usuario data);
}