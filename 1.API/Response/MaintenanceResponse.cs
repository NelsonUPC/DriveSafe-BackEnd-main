namespace _1.API.Response;

public class MaintenanceResponse
{
    public int Id { get; set; }
    public string type_problem { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int tenant_id { get; set; }
    public int owner_id { get; set; }
}