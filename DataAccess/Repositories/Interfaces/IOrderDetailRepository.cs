using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetOrderDetails(int orderId, string searchValue, int pageIndex, int pageSize, string orderBy);

        OrderDetail GetOrderDetailById(int orderId, int productId);
    }
}
