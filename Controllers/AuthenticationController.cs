using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DotNetCoreWebAPI.Controllers
{
    public class UserCredential
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    [ApiController]
    [Route("api/authentication")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost]
        public ActionResult<string> GetBearerToken([FromBody] UserCredential userCredential)
        {
            // Step 1: Validate username and password
            string username = this.configuration["Authentication:UserName"];
            string password = this.configuration["Authentication:Password"];

            if (!string.Equals(username, userCredential.Username)
                || !string.Equals(password, userCredential.Password))
            {
                return Unauthorized();
            }

            // Step 2: Generate a SymmetricSecurityKey instance from a private secured key
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(this.configuration["Authentication:SecretKey"]));

            // Step 3: Generate signing credentials for above key with a security algorithm
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Step 4: Create claims for the user
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, this.configuration["Authentication:Claims:UserName"]),
                new Claim(ClaimTypes.NameIdentifier, this.configuration["Authentication:Claims:Identifier"]),
                new Claim(ClaimTypes.GivenName, this.configuration["Authentication:Claims:FirstName"]),
                new Claim("CityName", this.configuration["Authentication:Claims:CityName"])
            };

            // Step 5: Create a JWT security token
            var token = new JwtSecurityToken(
                issuer: this.configuration["Authentication:Issuer"],
                audience: this.configuration["Authentication:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: signingCredentials
            );

            // Step 6: Return the Jwt Bearer Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}