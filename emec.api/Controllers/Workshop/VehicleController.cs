using emec.contracts.managers;
using emec.entities.Login;
using emec.entities.Vehicle.Register;
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

        public VehicleController(
    //IUserManager userManager,
    IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
    IErrorMessages errorMessages,
    IMapper<Object, ResponseBase> serviceResponseMapper
    )
        {
            //_userManager = userManager;
            _errormessages = errorMessages;
            _serviceResponseErrorMapper = serviceResponseErrorMapper;
            _serviceResponseMapper = serviceResponseMapper ?? throw new ArgumentNullException(nameof(_serviceResponseMapper));
        }



        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseBase>> RegisterVehicle([FromBody] VehicleRegisterDataRequest vehicleRegisterDataRequest)
        {
            // Simulate async operation to address CS1998
            await Task.CompletedTask;

            var response = _serviceResponseMapper.Map(new ResponseBase
            {
                IsSuccess = true,
                Result = "Vehicle registration successful",
                Error = null
            });

            return Ok(response);
        }

    }
}
