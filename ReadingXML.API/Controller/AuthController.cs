using Microsoft.AspNetCore.Mvc;
using ReadingXML.Application.Interfaces;

namespace ReadingXML.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            try
            {
                var response = await _authService.AuthAsync();
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Não foi possível autenticar, tente novamente mais tarde." });
            }
        }
    }
}
