using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetProducts(string searchValue, int pageIndex, int pageSize, string orderBy);

        Product GetProductById(int id);

        void SaveProduct(Product p);

        void UpdateProduct(Product p);

        void DeleteProduct(Product p);

    }
}
