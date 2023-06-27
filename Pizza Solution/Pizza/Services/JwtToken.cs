using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Pizza.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pizza.Services
{
    public class JwtToken : IJwtToken
    {
        private readonly IConfiguration _config;

        public JwtToken(IConfiguration configuration)
        {
            this._config = configuration;
        }

        // Create JWT Token, set user details as claims and role.
        public string CreateJwtToken(string email, string role)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, email));
            claims.Add(new Claim(ClaimTypes.Role, role));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30), // Set the initial expiration time to 30 minutes from now
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

