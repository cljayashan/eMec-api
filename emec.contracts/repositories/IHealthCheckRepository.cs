using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.entities.HealthCheck;

namespace emec.contracts.repositories
{
    public interface IHealthCheckRepository
    {
        Task<IEnumerable<HealthCheckDataResponse>> GetHealthCheckDataAsync(HealthCheckDataRequest healthCheckDataRequest);
    }
}
