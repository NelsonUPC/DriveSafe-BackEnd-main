namespace _3.Data.Models;

public class Maintenance : BaseModel
{
    public string type_problem { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int tenant_id { get; set; }
    public int owner_id { get; set; }
}