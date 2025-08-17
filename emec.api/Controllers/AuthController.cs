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
                var response = await _userManager.Authenticate(loginDataRequest);
                if (response.IsSuccess && response.Result is emec.entities.Login.LoginDataResponse loginDataResponse)
                {
                    // Set refresh token as HttpOnly, Secure cookie
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, // Should be true in production
                        SameSite = SameSiteMode.Strict, // Use None if API and client are on different domains
                        Expires = DateTime.UtcNow.AddSeconds(80) // Or use your configured refresh token expiry
                    };
                    Response.Cookies.Append("refreshToken", loginDataResponse.RefreshToken, cookieOptions);

                    // Remove refresh token from response body
                    loginDataResponse.RefreshToken = null!;
                }
                return response;
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
                // Read refresh token from cookie
                var refreshToken = Request.Cookies["refreshToken"];
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidOrExpiredRefreshToken());
                }
                var result = await _userManager.RefreshToken(request.Attributes.UserName, refreshToken);
                if (!result.IsSuccess)
                {
                    return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidOrExpiredRefreshToken());
                }
                else if (result.Result is emec.entities.Login.LoginDataResponse loginDataResponse)
                {
                    // Overwrite the refresh token cookie
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, // Should be true in production
                        SameSite = SameSiteMode.Strict, // Use None if API and client are on different domains
                        Expires = DateTime.UtcNow.AddSeconds(80) // Or use your configured refresh token expiry
                    };
                    Response.Cookies.Append("refreshToken", loginDataResponse.RefreshToken, cookieOptions);

                    // Remove refresh token from response body
                    loginDataResponse.RefreshToken = null!;
                }
                return result;
            }
            else
            {
                return BadRequest(_serviceResponseErrorMapper.Map(_errormessages.Common_ApiActionNotPermitted()));
            }

        }
    }

}
