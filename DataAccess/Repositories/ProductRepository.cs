using BusinessObject;
using DataAccess.Daos;
using DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product p) => ProductDAO.DeleteProduct(p);

        public Product GetProductById(int id) => ProductDAO.GetProductById(id);

        public List<Product> GetProducts(string searchValue, int pageIndex, int pageSize, string orderBy) => ProductDAO.GetProducts(searchValue, pageIndex, pageSize, orderBy);

        public void SaveProduct(Product p) => ProductDAO.SaveProduct(p);
        public void UpdateProduct(Product p) => ProductDAO.UpdateProduct(p);
    }
}
