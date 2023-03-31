using E_Book_Library.DTOs;

namespace E_Book_Library.Services
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> Register(RegisterDto model);
        Task<LoginResponseDto> Login(LoginDto model);
    }
}
