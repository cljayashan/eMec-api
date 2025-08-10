using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.shared.models;

namespace emec.entities.HealthCheck
{
    public class HealthCheckDataRequest : RequestBase
    {
        public required string Action { get; set; }
        public required HealthCheckDataAttributes Attributes { get; set; }
    }
    
}
