﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emec.entities.HealthCheck
{
    public class HealthCheckDataResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
