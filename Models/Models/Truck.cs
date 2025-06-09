using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VOLVO.Models
{
    public class Truck : Vehicle
    {
        public override string type => "Truck";
        public override int numberOfPassengers => 1;

        public Truck(ChassisId chassisId, string color)
            : base(chassisId, color)
        {
            this.chassisId = chassisId;
            this.color = color;
        }
    }
}