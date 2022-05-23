using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Daos
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                }
                return instance;
            }
        }

        public static List<Product> GetProducts(string searchValue, int pageIndex, int pageSize, string orderBy)
        {
            var products = new List<Product>();
            IQueryable<Product> query = null;

            try
            {
                using (var context = new FStoreDBContext())
                {
                    query = context.Products;

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.ProductId.ToString().Equals(searchValue)
                            || e.ProductName.Contains(searchValue)
                            || e.UnitPrice.ToString().Equals(searchValue)
                            || e.UnitsInStock.ToString().Equals(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.ProductId);
                                break;
                            case "categoryId":
                                query = query.OrderBy(e => e.CategoryId);
                                break;
                            case "categoryId desc":
                                query = query.OrderByDescending(e => e.CategoryId);
                                break;
                            case "productName":
                                query = query.OrderBy(e => e.ProductName);
                                break;
                            case "productName desc":
                                query = query.OrderByDescending(e => e.ProductName);
                                break;
                            case "unitPrice":
                                query = query.OrderBy(e => e.UnitPrice);
                                break;
                            case "unitPrice desc":
                                query = query.OrderByDescending(e => e.UnitPrice);
                                break;
                            case "unitsInStock":
                                query = query.OrderBy(e => e.UnitsInStock);
                                break;
                            case "unitsInStock desc":
                                query = query.OrderByDescending(e => e.UnitsInStock);
                                break;
                            default:
                                query = query.OrderBy(e => e.ProductId);
                                break;
                        }
                    }

                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(e => e.Category);

                    products = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public static Product GetProductById(int id)
        {
            var product = new Product();
            try
            {
                using (var context = new FStoreDBContext())
                {
                    product = context.Products.Where(e => e.ProductId == id).Include(e => e.Category).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public static void SaveProduct(Product product)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    product.ProductId = context.Products.Max(e => e.ProductId) + 1;
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateProduct(Product p)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteProduct(Product p)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    context.Products.Remove(p);
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
