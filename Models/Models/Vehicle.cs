using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VOLVO.Models
{
    public abstract class Vehicle : IVehicle
    {
        public ChassisId chassisId { get; set; }
        public abstract string type { get; }
        public abstract int numberOfPassengers { get; }
        public string color { get; set; }

        public string chassisNumberAndSeries { get { return this.chassisId.chassisSeries + this.chassisId.chassisNumber;  }  }

        protected Vehicle(ChassisId _chassisId, string _color)
        {
            chassisId = _chassisId;
            color = _color;
        }
    }
}