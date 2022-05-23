using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Daos
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();

        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                }
                return instance;
            }
        }

        public static List<Order> GetOrders(string searchValue, int pageIndex, int pageSize, string orderBy)
        {
            var orders = new List<Order>();
            IQueryable<Order> query = null;

            try
            {
                using (var context = new FStoreDBContext())
                {
                    query = context.Orders;

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.OrderId.ToString().Equals(searchValue)
                            || e.MemberId.ToString().Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.OrderId);
                                break;
                            case "memberId":
                                query = query.OrderBy(e => e.MemberId);
                                break;
                            case "memberId desc":
                                query = query.OrderByDescending(e => e.MemberId);
                                break;
                            case "orderDate":
                                query = query.OrderBy(e => e.OrderDate);
                                break;
                            case "orderName desc":
                                query = query.OrderByDescending(e => e.OrderDate);
                                break;
                            case "requiredDate":
                                query = query.OrderBy(e => e.RequiredDate);
                                break;
                            case "requiredDate desc":
                                query = query.OrderByDescending(e => e.RequiredDate);
                                break;
                            case "shippedDate":
                                query = query.OrderBy(e => e.ShippedDate);
                                break;
                            case "shippedDate desc":
                                query = query.OrderByDescending(e => e.ShippedDate);
                                break;
                            default:
                                query = query.OrderBy(e => e.OrderId);
                                break;
                        }
                    }

                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    orders = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public static List<Order> Report(DateTime? startDate, DateTime? endDate, string searchValue, int pageIndex, int pageSize, string orderBy)
        {
            var orders = new List<Order>();
            IQueryable<Order> query = null;

            try
            {
                using (var context = new FStoreDBContext())
                {
                    query = context.Orders;

                    if (startDate.HasValue)
                    {
                        query = query.Where(e => e.OrderDate >= startDate);
                    }
                    if (endDate.HasValue)
                    {
                        query = query.Where(e => e.OrderDate <= endDate);
                    }

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.OrderId.ToString().Equals(searchValue)
                            || e.MemberId.ToString().Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "orderDate desc":
                                query = query.OrderByDescending(e => e.OrderDate);
                                break;
                            case "memberId":
                                query = query.OrderBy(e => e.MemberId);
                                break;
                            case "memberId desc":
                                query = query.OrderByDescending(e => e.MemberId);
                                break;
                            case "id":
                                query = query.OrderBy(e => e.OrderId);
                                break;
                            case "id desc":
                                query = query.OrderByDescending(e => e.OrderId);
                                break;
                            case "requiredDate":
                                query = query.OrderBy(e => e.RequiredDate);
                                break;
                            case "requiredDate desc":
                                query = query.OrderByDescending(e => e.RequiredDate);
                                break;
                            case "shippedDate":
                                query = query.OrderBy(e => e.ShippedDate);
                                break;
                            case "shippedDate desc":
                                query = query.OrderByDescending(e => e.ShippedDate);
                                break;
                            default:
                                query = query.OrderByDescending(e => e.OrderDate);
                                break;
                        }
                    }

                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Include(e => e.OrderDetails)
                        .ThenInclude(e => e.Product);

                    orders = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        public static Order GetOrderById(int id)
        {
            var order = new Order();
            try
            {
                using (var context = new FStoreDBContext())
                {
                    order = context.Orders.Where(e => e.OrderId == id).Include(e => e.OrderDetails).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public static void SaveOrder(Order order)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    order.OrderId = context.Orders.Max(e => e.OrderId) + 1;
                    context.Orders.Add(order);
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
