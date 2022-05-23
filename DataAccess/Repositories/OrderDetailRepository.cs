using BusinessObject;
using DataAccess.Daos;
using DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public OrderDetail GetOrderDetailById(int orderId, int productId) => OrderDetailDAO.GetOrderDetailById(orderId, productId);

        public List<OrderDetail> GetOrderDetails(int orderId, string searchValue, int pageIndex, int pageSize, string orderBy) => OrderDetailDAO.GetOrderDetails(orderId, searchValue, pageIndex, pageSize, orderBy);
    }
}
