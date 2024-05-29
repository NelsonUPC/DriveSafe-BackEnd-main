namespace _1.API.Response;

public class RentResponse
{
    public int id { get; set; }
    public string status { get; set; }
    public DateOnly start_date { get; set; }
    public DateOnly end_date { get; set; }
    public int vehicle_id { get; set; }
    public int owner_id { get; set; }
    public int tenant_id { get; set; }
    public string pick_up_place { get; set; }
}