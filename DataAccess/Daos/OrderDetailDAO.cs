using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Daos
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                }
                return instance;
            }
        }

        public static List<OrderDetail> GetOrderDetails(int orderId, string searchValue, int pageIndex, int pageSize, string orderBy)
        {
            var orderDetails = new List<OrderDetail>();
            IQueryable<OrderDetail> query = null;

            try
            {
                using (var context = new FStoreDBContext())
                {
                    query = context.OrderDetails.Where(e => e.OrderId == orderId);

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.ProductId.ToString().Equals(searchValue)
                            || e.UnitPrice.ToString().Equals(searchValue)
                            || e.Quantity.ToString().Equals(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "productId desc":
                                query = query.OrderByDescending(e => e.ProductId);
                                break;
                            case "unitPrice":
                                query = query.OrderBy(e => e.UnitPrice);
                                break;
                            case "unitPrice desc":
                                query = query.OrderByDescending(e => e.UnitPrice);
                                break;
                            case "quantity":
                                query = query.OrderBy(e => e.Quantity);
                                break;
                            case "quantity desc":
                                query = query.OrderByDescending(e => e.Quantity);
                                break;
                            default:
                                query = query.OrderBy(e => e.ProductId);
                                break;
                        }
                    }

                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    orderDetails = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }

        public static OrderDetail GetOrderDetailById(int orderId, int productId)
        {
            var orderDetail = new OrderDetail();
            try
            {
                using (var context = new FStoreDBContext())
                {
                    orderDetail = context.OrderDetails.Where(e => e.OrderId == orderId && e.ProductId == productId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        //public static void SaveOrderDetail(OrderDetail orderDetail)
        //{
        //    try
        //    {
        //        using (var context = new FStoreDBContext())
        //        {
        //            context.OrderDetails.Add(orderDetail);
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
