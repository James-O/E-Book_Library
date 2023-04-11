using E_Book_Library.DTOs;
using Ebook.Service.Services.Interfases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Book_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;     
        }
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginDto loginDto)
        {
            var login = await _authService.Login(loginDto);
            return Ok(login);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            return Ok(result);
        }
    }
}
