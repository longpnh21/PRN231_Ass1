using BusinessObject;
using DataAccess.Daos;
using DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public void DeleteCategory(Category category) => CategoryDAO.DeleteCategory(category);

        public Category FindCategoryById(int id) => CategoryDAO.FindCategoryById(id);

        public List<Category> GetCategories(string searchValue, int pageIndex, int pageSize) => CategoryDAO.GetCategories(searchValue, pageIndex, pageSize);

        public void SaveCategory(Category category) => CategoryDAO.SaveCategory(category);

        public void UpdateCategory(Category category) => CategoryDAO.UpdateCategory(category);
    }
}
