using E_Book_Library.Models;

namespace E_Book_Library.IServices
{
    public interface ICategoryService
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
