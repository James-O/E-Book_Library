using E_Book_Library.Models;

namespace E_Book_Library.Services
{
    public interface IJWTService
    {
        Task<string> GenerateToken(User user);
    }
}
