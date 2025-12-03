using ReadingXML.Application.DTOs;

namespace ReadingXML.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> AuthAsync();
    }
}
