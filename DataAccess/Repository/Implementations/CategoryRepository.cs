using DataAccess;
using E_Book_Library.Models;
using Ebook.Service.Services.Interfases;

namespace Ebook.Service.Services.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public bool CategoryExists(int id)
        {
            return _appDbContext.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _appDbContext.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _appDbContext.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _appDbContext.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _appDbContext.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _appDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _appDbContext.Update(category);
            return Save();
        }
    }
}
