namespace _1.API.Request;

public class UsuarioRequest
{
    [Microsoft.Build.Framework.Required]
    public int Id { get; set; }
    [Microsoft.Build.Framework.Required]
    public string nombres { get; set; }
    [Microsoft.Build.Framework.Required]
    public string apellidos { get; set; }
    [Microsoft.Build.Framework.Required]
    public string fecha_nacimiento { get; set; }
    [Microsoft.Build.Framework.Required]
    public int telefono { get; set; }
    [Microsoft.Build.Framework.Required]
    public string correo { get; set; }
    [Microsoft.Build.Framework.Required]
    public string contrasenia { get; set; }
    [Microsoft.Build.Framework.Required]
    public string tipo { get; set; }
}