using System;
using VOLVO.Models;
using VOLVO.DAL;

namespace VOLVO.BLL;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public void CreateVehicle(Vehicle vehicle)
    {
        if (GetVehicleByChassisId(vehicle.chassisId.chassisSeries, vehicle.chassisId.chassisNumber) != null)
            throw new ArgumentException("An Vehicle with this chassis identification already exists");

        _vehicleRepository.CreateVehicle(vehicle);
    }

    public void ChangeColor(string chassisSeries, uint chassisNumber, string newColor)
    {
        _vehicleRepository.ChangeColor(chassisSeries, chassisNumber, newColor);
    }

    public Vehicle GetVehicleByChassisId(string chassisSeries, uint chassisNumber)
    {
        return _vehicleRepository.GetVehicleByChassisId(chassisSeries, chassisNumber);
    }

    public IEnumerable<Vehicle> ListVehicles()
    {
        return _vehicleRepository.ListVehicles();
    }
}
