namespace _3.Data.Models;

public class Usuario : BaseModel
{
    public string nombres { get; set; }
    public string apellidos { get; set; }
    public string fecha_nacimiento { get; set; }
    public int telefono { get; set; }
    public string correo { get; set; }
    public string contrasenia { get; set; }
    public string tipo { get; set; }
}