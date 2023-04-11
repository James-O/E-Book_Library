using E_Book_Library.Models;

namespace Ebook.Service.Services.Interfases
{
    public interface IJWTService
    {
        Task<string> GenerateToken(User user);
    }
}
