using E_Book_Library.DTOs;

namespace Ebook.Service.Services.Interfases
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> Register(RegisterDto model);
        Task<LoginResponseDto> Login(LoginDto model);
    }
}
