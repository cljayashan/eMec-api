using emec.contracts.managers;
using emec.entities.Vehicle.List;
using emec.entities.Vehicle.Register;
using emec.shared.common;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<ResponseBase>> RegisterVehicle([FromBody] VehicleRegisterDataRequest vehicleRegisterDataRequest)
        {
            try
            {
                if (vehicleRegisterDataRequest.Action.Equals(Constants.ApiActions.Add))
                {
                    return await _vehicleManager.RegisterVehicle(vehicleRegisterDataRequest);
                }

                return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidRequest());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpPost("list")]
        public async Task<ActionResult<ResponseBase>> GetAllVehicles([FromBody] VehicleListDataRequest request)
        {
            try
            {
                if (request.Action.Equals(Constants.ApiActions.List))
                {
                    return await _vehicleManager.GetAllVehiclesAsync(request);
                }

                return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidRequest());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidRequest());
            }
        }

        [HttpPost("test")]
        [Authorize]
        public ActionResult<ResponseBase> Test()
        {
            var response = new ResponseBase
            {
                IsSuccess = true,
                Result = "VehicleController test endpoint is working.",
                Error = null
            };
            return Ok(response);
        }

    }
}
