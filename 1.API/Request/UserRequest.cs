namespace _1.API.Request;

public class UserRequest
{
    [Microsoft.Build.Framework.Required]
    public int id { get; set; }
    [Microsoft.Build.Framework.Required]
    public string name { get; set; }
    [Microsoft.Build.Framework.Required]
    public string last_name { get; set; }
    [Microsoft.Build.Framework.Required]
    public string birthdate { get; set; }
    [Microsoft.Build.Framework.Required]
    public int cellphone { get; set; }
    [Microsoft.Build.Framework.Required]
    public string gmail { get; set; }
    [Microsoft.Build.Framework.Required]
    public string password { get; set; }
    [Microsoft.Build.Framework.Required]
    public string type { get; set; }
}