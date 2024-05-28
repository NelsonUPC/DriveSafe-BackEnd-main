using _3.Data;
using _3.Data.Models;

namespace _2.Domain;

public class UsuarioDomain : IUsuarioDomain
{
    private IUsuarioData _usuarioData;
    public UsuarioDomain(IUsuarioData usuarioData)
    {
        _usuarioData = usuarioData;
    }
    public async Task<int> SaveAsync(Usuario data)
    {
        return await _usuarioData.SaveAsync(data);
    }
}