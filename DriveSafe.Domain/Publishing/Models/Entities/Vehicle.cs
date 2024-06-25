namespace DriveSafe.Domain.Publishing.Models.Entities;

public class Vehicle : BaseModel
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int MaximumSpeed { get; set; }
    public int Consumption { get; set; }
    public string Dimensions { get; set; }

}
