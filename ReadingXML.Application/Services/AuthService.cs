using ReadingXML.Application.DTOs;
using ReadingXML.Application.Interfaces;

namespace ReadingXML.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwt;

        public AuthService(JwtService jwt)
        {
            _jwt = jwt;
        }

        public async Task<AuthResponseDTO> AuthAsync()
        {
            var (token, expires) = _jwt.GenerateToken();
            return new AuthResponseDTO { Token = token, DataExpiracao = expires };
        }
    }
}
