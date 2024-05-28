using _3.Data;
using _3.Data.Models;

namespace _2.Domain;

public class VehicleDomain : IVehicleDomain
{
    private IVehicleData _vehicleData;
    public VehicleDomain(IVehicleData vehicleData)
    {
        _vehicleData = vehicleData;
    }
    public async Task<int> SaveAsync(Vehicle data)
    {
        return await _vehicleData.SaveAsync(data);
    }
}