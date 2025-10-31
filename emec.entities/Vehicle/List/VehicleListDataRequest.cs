using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emec.entities.Vehicle.List
{
    public class VehicleListDataRequest
    {
        public required string Action { get; set; }
        public required string[]? Args { get; set; }
        public required VehicleListDataAttribute Attributes { get; set; }
    }
}
