using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
            _userRepository = userRepository ?? throw new System.ArgumentNullException(nameof(userRepository));
            _serviceResponseMapper = serviceResponseMapper ?? throw new System.ArgumentNullException(nameof(serviceResponseMapper));
            _userDataRequestValidator = userDataRequestValidator ?? throw new System.ArgumentNullException(nameof(userDataRequestValidator));
            _serviceResponseErrorMapper = serviceResponseErrorMapper ?? throw new System.ArgumentNullException(nameof(serviceResponseErrorMapper));
            _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        public async Task<IList<string>> GetUserRolesAsync(string userName)
        {
            return await _userRepository.GetUserRolesAsync(userName);
        }

        private int GetAccessTokenValidity()
        {
            var value = _configuration["Jwt:AccessTokenValidity"];
            var ex= int.TryParse(value, out var seconds) ? seconds : 30;
            return ex;
        }

        private int GetRefreshTokenValidity()
        {
            var value = _configuration["Jwt:RefreshTokenValidity"];
            return int.TryParse(value, out var seconds) ? seconds : 60;
        }

        private string GenerateJwtToken(string username, IList<string> roles)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key configuration is missing.");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
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
                expires: DateTime.Now.AddSeconds(GetAccessTokenValidity()),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public async Task<ResponseBase> Authenticate(LoginDataRequest loginDataRequest)
        {
            var res = await _userRepository.ValidateUserAsync(loginDataRequest.Attributes.UserName, loginDataRequest.Attributes.Password);
            if (!res)
            {
                var loginRes = new ResponseMessage
                {
                    Message = "Invalid username or password."
                };
                return _serviceResponseErrorMapper.Map(loginRes);
            }
            else
            {
                var roles = await _userRepository.GetUserRolesAsync(loginDataRequest.Attributes.UserName);
                var token = GenerateJwtToken(loginDataRequest.Attributes.UserName, roles);
                var refreshToken = GenerateRefreshToken();
                await _userRepository.StoreRefreshTokenAsync(loginDataRequest.Attributes.UserName, refreshToken, DateTime.Now.AddSeconds(GetRefreshTokenValidity()));
                var loginRes = new LoginDataResponse
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                };
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + $"Generated JWT Token: {token}");
                return _serviceResponseMapper.Map(loginRes);
            }
        }

        public async Task<ResponseBase> RefreshToken(string userName, string refreshToken)
        {
            var valid = await _userRepository.ValidateRefreshTokenAsync(userName, refreshToken);
            if (!valid)
            {
                var error = new ResponseMessage
                {
                    Message = "Invalid or expired refresh token."
                };
                return _serviceResponseErrorMapper.Map(error);
            }
            var roles = await _userRepository.GetUserRolesAsync(userName);
            var newToken = GenerateJwtToken(userName, roles);
            var newRefreshToken = GenerateRefreshToken();
            await _userRepository.StoreRefreshTokenAsync(userName, newRefreshToken, DateTime.Now.AddSeconds(GetRefreshTokenValidity()));
            var response = new LoginDataResponse
            {
                AccessToken = newToken,
                RefreshToken = newRefreshToken
            };
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + $"Refreshed JWT Token: {newToken}");
            return _serviceResponseMapper.Map(response);
        }

        //public Task<bool> ValidateUserAsync(string userName, string password, DateTime dateTime)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

