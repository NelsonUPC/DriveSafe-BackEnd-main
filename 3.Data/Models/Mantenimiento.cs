namespace _3.Data.Models;

public class Mantenimiento : BaseModel
{
    public string tipo_problema { get; set; }
    public string titulo { get; set; }
    public string descripcion { get; set; }
    public int arrendatario_id { get; set; }
    public int propietario_id { get; set; }
}