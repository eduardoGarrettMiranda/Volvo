using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VOLVO.Models
{
    public class Bus : Vehicle
    {
        public override string type => "Bus";
        public override int numberOfPassengers => 42;

        public Bus(ChassisId chassisId, string color)
            : base(chassisId, color)
        {
            this.chassisId = chassisId;
            this.color = color;
        }
    }
}