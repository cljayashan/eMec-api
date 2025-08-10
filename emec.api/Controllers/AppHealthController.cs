using emec.contracts.managers;
using emec.entities.HealthCheck;
using emec.shared.common;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace emec.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AppHealthController : ControllerBase
    {

        private readonly IMapper<ResponseMessage, ResponseBase> _serviceResponseErrorMapper;
        private readonly IHealthCheckManager _healthCheckManager;
        private readonly ILogger<AppHealthController> _logger;
        private readonly IErrorMessages _errormessages;

        public AppHealthController(
            IHealthCheckManager healthCheckManager,
            IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
            ILogger<AppHealthController> logger,
            IErrorMessages errormessages
            )
        {
            _healthCheckManager = healthCheckManager;
            _serviceResponseErrorMapper = serviceResponseErrorMapper;
            _logger = logger;
            _errormessages = errormessages;
        }

        [HttpPost("check")]
        public async Task<ActionResult<ResponseBase>> CheckHealth([FromBody] HealthCheckDataRequest healthCheckDataRequest)
        {
            try
            {
                if (healthCheckDataRequest.Action.Equals(Constants.ApiActions.View))
                {
                    return await _healthCheckManager.GetHealth(healthCheckDataRequest);
                }
                else 
                {
                    return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidRequest());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }
    }
}
