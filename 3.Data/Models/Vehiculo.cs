namespace _3.Data.Models;

public class Vehiculo : BaseModel
{
    public string marca { get; set; }
    public string modelo { get; set; }
    public int velocidad_maxima { get; set; }
    public int consumo { get; set; }
    public string dimensiones { get; set; }
    public int peso { get; set; }
    public string clase { get; set; }
    public string transmision { get; set; }
    public string tipo_tiempo { get; set; }
    public int costo_alquiler { get; set; }
    public string lugar_recojo { get; set; }
    public string url_imagen { get; set; }
    public string estado_renta { get; set; }
    public int propietario_id { get; set; }
}