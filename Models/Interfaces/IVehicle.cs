using System;

namespace VOLVO.Models;

public interface IVehicle
{
    ChassisId chassisId { get; }
    string type { get; }
    int numberOfPassengers { get; }
    string color { get; }
}
