using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.entities.HealthCheck;

namespace emec.entities.Vehicle.Register
{
    public class VehicleRegisterDataRequest
    {
        public required string Action { get; set; }
        public required VehicleRegisterDataAttributes Attributes { get; set; }
    }
}
