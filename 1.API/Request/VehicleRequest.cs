namespace _1.API.Request;

public class VehicleRequest
{
    [Microsoft.Build.Framework.Required]
    public int Id { get; set; }
    [Microsoft.Build.Framework.Required]
    public string brand { get; set; }
    [Microsoft.Build.Framework.Required]
    public string model { get; set; }
    [Microsoft.Build.Framework.Required]
    public int maximum_speed { get; set; }
    [Microsoft.Build.Framework.Required]
    public int consumption { get; set; }
    [Microsoft.Build.Framework.Required]
    public string dimensions { get; set; }
    [Microsoft.Build.Framework.Required]
    public int weight { get; set; }
    [Microsoft.Build.Framework.Required]
    public string car_class { get; set; }
    [Microsoft.Build.Framework.Required]
    public string transmission { get; set; }
    [Microsoft.Build.Framework.Required]
    public string time_type { get; set; }
    [Microsoft.Build.Framework.Required]
    public int rental_cost { get; set; }
    [Microsoft.Build.Framework.Required]
    public string pick_up_place { get; set; }
    [Microsoft.Build.Framework.Required]
    public string url_image { get; set; }
    [Microsoft.Build.Framework.Required]
    public string rent_status { get; set; }
    [Microsoft.Build.Framework.Required]
    public int owner_id { get; set; }
}