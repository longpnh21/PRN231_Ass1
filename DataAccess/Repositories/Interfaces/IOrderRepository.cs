using BusinessObject;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOrders(string searchValue, int pageIndex, int pageSize, string orderBy);

        Order GetOrderById(int id);

        void SaveOrder(Order order);

        List<Order> Report(DateTime? startDate, DateTime? endDate, string searchValue, int pageIndex, int pageSize, string orderBy);
    }
}
