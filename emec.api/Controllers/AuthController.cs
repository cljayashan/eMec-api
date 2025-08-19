using emec.contracts.managers;
using emec.entities.Login;
using emec.shared.common;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;
using Microsoft.AspNetCore.Mvc;

namespace emec.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper<ResponseMessage, ResponseBase> _serviceResponseErrorMapper;
        private readonly IErrorMessages _errormessages;
        private readonly IMapper<Object, ResponseBase> _serviceResponseMapper;

        public AuthController(
            IUserManager userManager,
            IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
            IErrorMessages errorMessages,
            IMapper<Object, ResponseBase> serviceResponseMapper
            )
        {
            _userManager = userManager;
            _errormessages = errorMessages;
            _serviceResponseErrorMapper = serviceResponseErrorMapper;
            _serviceResponseMapper = serviceResponseMapper ?? throw new ArgumentNullException(nameof(_serviceResponseMapper));
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

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ResponseBase>> Refresh([FromBody] TokenRefreshRequest request)
        {
            if (request.Action == Constants.ApiActions.Authenticate)
            {
                var result = await _userManager.RefreshToken(request.Attributes.UserName, request.Attributes.RefreshToken);
                if (!result.IsSuccess)
                {
                    //return Unauthorized(result.Error);
                    return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidOrExpiredRefreshToken());
                }
                else
                {
                    //return Ok(result.Result);
                    return _serviceResponseMapper.Map(result.Result);
                }
            }
            else
            {
                return BadRequest(_serviceResponseErrorMapper.Map(_errormessages.Common_ApiActionNotPermitted()));

            }

        }
    }

}
