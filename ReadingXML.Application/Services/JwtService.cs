using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReadingXML.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReadingXML.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config) => _config = config;

        public (string token, DateTime expiresAt) GenerateToken()
        {
            var key = _config["Jwt:Key"] ?? throw new Exception("JWT Key não configurada");
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var hour = int.Parse(_config["Jwt:ExpiresHours"] ?? "2");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddHours(hour);

            var claims = new List<Claim>();

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenString, expires);
        }
    }
}
