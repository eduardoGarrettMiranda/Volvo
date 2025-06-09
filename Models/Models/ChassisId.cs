using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VOLVO.Models
{
    public class ChassisId
    {
        public string chassisSeries { get; }
        public uint chassisNumber { get; }

        public ChassisId(string _chassisSeries, uint _chassisNumber)
        {
            if (string.IsNullOrWhiteSpace(_chassisSeries))
                throw new ArgumentException("Chassis series cannot be empty.");

            chassisSeries = _chassisSeries;
            chassisNumber = _chassisNumber;
        }
    }
}