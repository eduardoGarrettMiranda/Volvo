using System;
using VOLVO.Models;

namespace VOLVO.BLL;

public interface IVehicleService
{
    void CreateVehicle(Vehicle vehicle);
    Vehicle GetVehicleByChassisId(string chassisSeries, uint chassisNumber);
    IEnumerable<Vehicle> ListVehicles();
    void ChangeColor(string chassisSeries, uint chassisNumber, string color);
}

