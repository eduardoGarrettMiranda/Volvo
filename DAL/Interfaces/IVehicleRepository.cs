using System;
using VOLVO.Models;

namespace VOLVO.DAL;

public interface IVehicleRepository
{
    void CreateVehicle(Vehicle vehicle);
    Vehicle GetVehicleByChassisId(string chassisSeries, uint chassisNumber);
    IEnumerable<Vehicle> ListVehicles();
    void ChangeColor(string chassisSeries, uint chassisNumber, string newColor);
    void CreateDatabaseAndTable();
}
