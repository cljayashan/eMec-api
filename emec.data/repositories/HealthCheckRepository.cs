using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.contracts.repositories;
using emec.dbcontext.tables.Models;
using emec.entities.HealthCheck;
using emec.dbcontext.tables.Models;

namespace emec.data.repositories
{
    public class HealthCheckRepository : IHealthCheckRepository
    {
        private readonly EMecContext _emecContext;

        public HealthCheckRepository(EMecContext emecContext)
        {
            _emecContext = emecContext ?? throw new ArgumentNullException(nameof(emecContext));
        }

        public Task<IEnumerable<HealthCheckDataResponse>> GetHealthCheckDataAsync(HealthCheckDataRequest healthCheckDataRequest)
        {
            var health = new List<HealthCheckDataResponse>
            {
                new HealthCheckDataResponse
                {
                    Status = "Healthy",
                    Message = "All systems operational",
                    Timestamp = DateTime.UtcNow
                }
            };

            //var health = from hc in _emecContext.HealthChecks
            //             where hc.Id == 1
            //             select new HealthCheckDataResponse
            //             {
            //                 Status = hc.Status,
            //                 Message = "Aadasdada",
            //                 Timestamp = DateTime.Now
            //             };

            return Task.FromResult<IEnumerable<HealthCheckDataResponse>>(health);
        }

    }
}
