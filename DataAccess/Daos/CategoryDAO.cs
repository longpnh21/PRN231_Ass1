using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Daos
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                }
                return instance;
            }
        }

        public static List<Category> GetCategories(string searchValue, int pageIndex, int pageSize)
        {
            var categories = new List<Category>();
            int count = 0;
            IQueryable<Category> query = null;
            try
            {
                using (var context = new FStoreDBContext())
                {
                    query = context.Categories;
                    count = query.Count();
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.CategoryName.Contains(searchValue));
                    }

                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    categories = query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Category>(categories, count, pageSize, pageSize);
        }

        public static Category FindCategoryById(int id)
        {
            var category = new Category();
            try
            {
                using (var context = new FStoreDBContext())
                {
                    category = context.Categories.SingleOrDefault(e => e.CategoryId.Equals(id));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        public static void SaveCategory(Category category)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    category.CategoryId = context.Categories.Max(e => e.CategoryId) + 1;
                    context.Categories.Add(category);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCategory(Category category)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    context.Entry<Category>(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCategory(Category category)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    var c = context.Categories.SingleOrDefault(e => e.CategoryId.Equals(category.CategoryId));
                    if (c != null)
                    {
                        context.Categories.Remove(c);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
