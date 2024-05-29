namespace _1.API.Request;

public class MaintenanceRequest
{
    [Microsoft.Build.Framework.Required]
    public int id { get; set; }
    [Microsoft.Build.Framework.Required]
    public string type_problem { get; set; }
    [Microsoft.Build.Framework.Required]
    public string title { get; set; }
    [Microsoft.Build.Framework.Required]
    public string description { get; set; }
    [Microsoft.Build.Framework.Required]
    public int tenant_id { get; set; }
    [Microsoft.Build.Framework.Required]
    public int owner_id { get; set; }
}