using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Token.Models;

namespace Token.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
    {
        // Step 1: validate the username/password
        var userInfo = ValidateUser(authenticationRequestBody);
        if (userInfo == null)
            return Unauthorized();
        
        // Step 2: create a JWT token
        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
        var signingCredentials = new SigningCredentials(
            securityKey, 
            SecurityAlgorithms.HmacSha256);
        
        // Step 3: Create Claims
        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("sub", userInfo.UserId.ToString())); // Technical identifier of the user
        claimsForToken.Add(new Claim("given_name", userInfo.FirstName));
        claimsForToken.Add(new Claim("family_name", userInfo.LastName));
        claimsForToken.Add(new Claim("city", userInfo.City));

        // Step 4: Create a JWT Token
        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            signingCredentials);
        
        var tokenToReturn = new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);
            
        return Ok(tokenToReturn);
    }

    private UserInfo ValidateUser(AuthenticationRequestBody authenticationRequestBody)
    {
        // Some logic to validate username and password against DB etc.
        // Assuming the user is valid and this method doesn't throw any exceptions.
        // Returns the UserInfo object
        return new UserInfo(
            1, 
            authenticationRequestBody.UserName ?? "", 
            "John",
            "Doe",
            "Pune");
    }
}
