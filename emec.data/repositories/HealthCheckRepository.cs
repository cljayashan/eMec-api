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
        private readonly eMecContext _emecContext;

        public HealthCheckRepository(eMecContext emecContext)
        {
            _emecContext = emecContext ?? throw new ArgumentNullException(nameof(emecContext));
        }

        public Task<IEnumerable<HealthCheckDataResponse>> GetHealthCheckDataAsync(HealthCheckDataRequest healthCheckDataRequest)
        {

            var health = from hc in _emecContext.TblHealthChecks
                         where hc.Id == 1
                         select new HealthCheckDataResponse
                         {
                             Status = hc.Status,
                             Message = "Db connection ok",
                             Timestamp = DateTime.Now
                         };

            return Task.FromResult<IEnumerable<HealthCheckDataResponse>>(health);
        }

    }
}
