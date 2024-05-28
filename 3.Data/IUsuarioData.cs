using _3.Data.Models;

namespace _3.Data;

public interface IUsuarioData
{
    Task<int> SaveAsync(Usuario data);
    Task<List<Usuario>> getAllAsync();
}