using System;
using VOLVO.DAL;
using VOLVO.Models;

namespace VOLVO.Tests;

public class FakeRepository : IVehicleRepository
{
    private readonly List<Vehicle> _vehicles = new List<Vehicle>();

    public void CreateVehicle(Vehicle vehicle)
    {
        _vehicles.Add(vehicle);
    }

    public void ChangeColor(string chassisSeries, uint chassisNumber, string newColor)
    {
        var vehicle = GetVehicleByChassisId(chassisSeries, chassisNumber);
        vehicle.color = newColor;
    }

    public Vehicle GetVehicleByChassisId(string chassisSeries, uint chassisNumber)
    {
        return _vehicles.FirstOrDefault(v =>
            v.chassisId.chassisSeries == chassisSeries &&
            v.chassisId.chassisNumber == chassisNumber);
    }

    public IEnumerable<Vehicle> ListVehicles()
    {
        return _vehicles;
    }

    public void CreateDatabaseAndTable()
    {
        throw new NotImplementedException();
    }
}

