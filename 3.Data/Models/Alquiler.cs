namespace _3.Data.Models;

public class Alquiler : BaseModel
{
    public string Estado { get; set; }
    public DateOnly fechaInicio { get; set; }
    public DateOnly fechaFin { get; set; }
    public int vehiculoId { get; set; }
    public int propietario_id { get; set; }
    public int arrendatario_id { get; set; }
    public string lugar { get; set; }
}