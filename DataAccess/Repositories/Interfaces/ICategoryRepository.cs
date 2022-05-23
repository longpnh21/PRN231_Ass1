using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories(string searchValue, int pageIndex, int pageSize);

        Category FindCategoryById(int id);

        void SaveCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);
    }
}
