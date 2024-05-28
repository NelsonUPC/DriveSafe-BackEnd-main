namespace _3.Data.Models;

public class Rent : BaseModel
{
    public string status { get; set; }
    public DateOnly start_date { get; set; }
    public DateOnly end_date { get; set; }
    public int vehicle_id { get; set; }
    public int owner_id { get; set; }
    public int tenant_id { get; set; }
    public string pick_up_place { get; set; }
}