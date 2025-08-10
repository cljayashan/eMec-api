using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.entities.HealthCheck;
using emec.shared.models;

namespace emec.contracts.managers
{
    public interface IHealthCheckManager
    {
        Task<ResponseBase> GetHealth(HealthCheckDataRequest healthCheckDataRequest);
    }
}
