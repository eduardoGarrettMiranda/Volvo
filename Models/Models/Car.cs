using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VOLVO.Models
{
    public class Car : Vehicle
    {
        public override string type => "Car";
        public override int numberOfPassengers => 4;
        
        public Car(ChassisId chassisId, string color)
           : base(chassisId, color)
        {
            this.chassisId = chassisId;
            this.color = color;
        }
    }
}