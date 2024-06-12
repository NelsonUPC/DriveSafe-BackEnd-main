using System.ComponentModel.DataAnnotations;

namespace DriveSafe.Presentation.Request;

public class UserRequest
{
    [Microsoft.Build.Framework.Required]
    public string Name { get; set; }
    [Microsoft.Build.Framework.Required]
    public string LastName { get; set; }
    [Microsoft.Build.Framework.Required]
    public DateOnly Birthdate { get; set; }
    [Microsoft.Build.Framework.Required]
    public int cellphone { get; set; }
    [Microsoft.Build.Framework.Required]
    public string Gmail { get; set; }
    [Microsoft.Build.Framework.Required]
    public string Password { get; set; }
    [Microsoft.Build.Framework.Required]
    public string Type { get; set; }
}