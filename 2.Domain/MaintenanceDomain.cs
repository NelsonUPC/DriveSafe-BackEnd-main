using _3.Data;
using _3.Data.Models;

namespace _2.Domain;

public class MaintenanceDomain : IMaintenanceDomain
{
    private IMaintenanceData _maintenanceData;
    public MaintenanceDomain(IMaintenanceData maintenanceData)
    {
        _maintenanceData = maintenanceData;
    }
    public async Task<int> SaveAsync(Maintenance data)
    {
        return await _maintenanceData.SaveAsync(data);
    }
}