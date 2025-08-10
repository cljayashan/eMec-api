using emec.contracts.managers;
using emec.entities.Login;
using emec.shared.common;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;
using Microsoft.AspNetCore.Mvc;

namespace emec.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserManager _userManager;
        private readonly IMapper<ResponseMessage, ResponseBase> _serviceResponseErrorMapper;
        private readonly IErrorMessages _errormessages;

        public AuthController(
            IConfiguration configuration,
            IUserManager userManager,
            IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
            IErrorMessages errorMessages
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _errormessages = errorMessages;
            _serviceResponseErrorMapper = serviceResponseErrorMapper;
        }

        [HttpPost("auth")]
        public async Task<ActionResult<ResponseBase>> Authenticate([FromBody] LoginDataRequest loginDataRequest)
        {
            if (loginDataRequest.Action == Constants.ApiActions.Authenticate)
            {
                return await _userManager.Authenticate(loginDataRequest);
            }
            else
            {
                return _serviceResponseErrorMapper.Map(_errormessages.Common_ApiActionNotPermitted());
            }
        }        
    }
}
