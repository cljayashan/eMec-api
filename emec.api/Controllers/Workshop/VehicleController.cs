using emec.contracts.managers;
using emec.entities.HealthCheck;
using emec.entities.Login;
using emec.entities.Vehicle.Register;
using emec.shared.common;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace emec.api.Controllers.Workshop
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {

        //private readonly IUserManager _userManager;
        private readonly IMapper<ResponseMessage, ResponseBase> _serviceResponseErrorMapper;
        private readonly IErrorMessages _errormessages;
        private readonly IMapper<Object, ResponseBase> _serviceResponseMapper;
        private readonly IVehicleManager _vehicleManager;
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(
            IVehicleManager vehicleManager,
            IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
            IErrorMessages errorMessages,
            IMapper<Object, ResponseBase> serviceResponseMapper,
             ILogger<VehicleController> logger
        )
        {
            _vehicleManager = vehicleManager;
            _errormessages = errorMessages;
            _serviceResponseErrorMapper = serviceResponseErrorMapper;
            _serviceResponseMapper = serviceResponseMapper ?? throw new ArgumentNullException(nameof(_serviceResponseMapper));
            _logger = logger;
        }



        [HttpPost("register")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseBase>> RegisterVehicle([FromBody] VehicleRegisterDataRequest vehicleRegisterDataRequest)
        {
            try
            {
                if (vehicleRegisterDataRequest.Action.Equals(Constants.ApiActions.Add))
                {
                    return await _vehicleManager.RegisterVehicle(vehicleRegisterDataRequest);
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
            //return Ok(response);
        }

    }
}
