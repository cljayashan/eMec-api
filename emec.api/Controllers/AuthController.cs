using emec.entities.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace emec.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDataRequest request)
        {
            // Validate user credentials (this is a placeholder, replace with actual validation logic)  
            if (request.Attributes.UserName == "a" && request.Attributes.Password == "a")
            {
                var token = GenerateJwtToken(request.Attributes.UserName);
                return Ok(new { Token = token });
            }
            if (request.Attributes.UserName == "u" && request.Attributes.Password == "u")
            {
                var token = GenerateJwtToken(request.Attributes.UserName);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password.");
        }

        private string GenerateJwtToken(string username)
        {
            // Fetch user roles from your database here. For example:
            var adminRoles = new List<string> { "Admin" }; // Replace with actual DB lookup
            var userRoles = new List<string> { "User" }; // Replace with actual DB lookup

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, username),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
           };

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

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
