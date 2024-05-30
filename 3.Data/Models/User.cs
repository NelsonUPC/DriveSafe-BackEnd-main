namespace _3.Data.Models;

public class User : BaseModel
{
    public string name { get; set; }
    public string last_name { get; set; }
    public DateOnly birthdate { get; set; }
    public int cellphone { get; set; }
    public string gmail { get; set; }
    public string password { get; set; }
    public string type { get; set; }
}