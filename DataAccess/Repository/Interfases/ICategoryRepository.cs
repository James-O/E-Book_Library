using E_Book_Library.Models;

namespace Ebook.Service.Services.Interfases
{
    public interface ICategoryRepository
    {
        //Get
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        //Post
        bool CreateCategory(Category category);
        //put
        bool UpdateCategory(Category category);
        //delete
        bool DeleteCategory(Category category);
        //Others
        bool CategoryExists(int id);
        bool Save();
    }
}
