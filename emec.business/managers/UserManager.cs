using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using emec.contracts.managers;
using emec.contracts.repositories;
using emec.entities.Login;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace emec.business.managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper<Object, ResponseBase> _serviceResponseMapper;

        private readonly IValidator<LoginDataRequest> _userDataRequestValidator;

        private readonly IMapper<ResponseMessage, ResponseBase> _serviceResponseErrorMapper;

        private readonly IConfiguration _configuration;

        public UserManager(
            IUserRepository userRepository,
             IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
            IValidator<LoginDataRequest> userDataRequestValidator,
            IMapper<Object, ResponseBase> serviceResponseMapper,
            IConfiguration configuration
            )
        {
            _userRepository = userRepository;
            _serviceResponseMapper = serviceResponseMapper ?? throw new System.ArgumentNullException(nameof(serviceResponseMapper));
            _userDataRequestValidator = userDataRequestValidator ?? throw new System.ArgumentNullException(nameof(userDataRequestValidator));
            _serviceResponseErrorMapper = serviceResponseErrorMapper ?? throw new System.ArgumentNullException(nameof(serviceResponseErrorMapper));
            _configuration = configuration;
        }

        public async Task<IList<string>> GetUserRolesAsync(string userName)
        {
            return await _userRepository.GetUserRolesAsync(userName);
        }

        public async Task<ResponseBase> Authenticate(LoginDataRequest loginDataRequest)
        {
            var res = await _userRepository.ValidateUserAsync(loginDataRequest.Attributes.UserName, loginDataRequest.Attributes.Password);
            //Login failed
            if (!res)
            {
                var loginRes = new ResponseMessage
                {
                    Message = "Invalid username or password."

                };
                return _serviceResponseErrorMapper.Map(loginRes);
            }
            //Login successful
            else
            {
                var roles = await _userRepository.GetUserRolesAsync(loginDataRequest.Attributes.UserName);
                var token = GenerateJwtToken(loginDataRequest.Attributes.UserName, roles);
                var loginRes = new LoginDataResponse
                {
                     Token = token
                };
                return _serviceResponseMapper.Map(loginRes);
            }
        }

        private string GenerateJwtToken(string username, IList<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}

