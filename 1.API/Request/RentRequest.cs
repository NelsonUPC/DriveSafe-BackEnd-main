namespace _1.API.Request;

public class RentRequest
{
    [Microsoft.Build.Framework.Required]
    public string status { get; set; }
    [Microsoft.Build.Framework.Required]
    public DateOnly start_date { get; set; }
    [Microsoft.Build.Framework.Required]
    public DateOnly end_date { get; set; }
    [Microsoft.Build.Framework.Required]
    public int vehicle_id { get; set; }
    [Microsoft.Build.Framework.Required]
    public int owner_id { get; set; }
    [Microsoft.Build.Framework.Required]
    public int tenant_id { get; set; }
    [Microsoft.Build.Framework.Required]
    public string pick_up_place { get; set; }
}