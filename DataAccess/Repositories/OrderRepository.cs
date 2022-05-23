using BusinessObject;
using DataAccess.Daos;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Order GetOrderById(int id) => OrderDAO.GetOrderById(id);

        public List<Order> GetOrders(string searchValue, int pageIndex, int pageSize, string orderBy) => OrderDAO.GetOrders(searchValue, pageIndex, pageSize, orderBy);

        public List<Order> Report(DateTime? startDate, DateTime? endDate, string searchValue, int pageIndex, int pageSize, string orderBy)
            => OrderDAO.Report(startDate, endDate, searchValue, pageIndex, pageSize, orderBy);

        public void SaveOrder(Order order) => OrderDAO.SaveOrder(order);
    }
}
