using System;

namespace VOLVO.Models;

public class VehicleFactory : IVehicleFactory
{
    public Vehicle CreateVehicle(string type, ChassisId chassisId, string color)
    {
        return type switch
        {
            "Car" => new Car(chassisId, color),
            "Bus" => new Bus(chassisId, color),
            "Truck" => new Truck(chassisId, color),
            _ => throw new ArgumentException($"Unknown vehicle type: {type}")
        };
    }
}