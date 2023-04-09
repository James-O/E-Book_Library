using E_Book_Library.Models;

namespace E_Book_Library.IServices
{
    public interface IJWTService
    {
        Task<string> GenerateToken(User user);
    }
}
