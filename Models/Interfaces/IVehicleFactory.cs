using System;

namespace VOLVO.Models;

public interface IVehicleFactory
{
    Vehicle CreateVehicle(string type, ChassisId ChassisId, string color);
}
