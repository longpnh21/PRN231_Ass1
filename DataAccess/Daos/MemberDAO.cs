using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Daos
{
    internal class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                }
                return instance;
            }
        }

        public static List<Member> GetMembers(string searchValue, int pageIndex, int pageSize, string orderBy)
        {
            var members = new List<Member>();
            int count = 0;
            IQueryable<Member> query = null;

            try
            {
                using (var context = new FStoreDBContext())
                {
                    query = context.Members;
                    count = query.Count();
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.MemberId.ToString().Equals(searchValue)
                        || e.Email.Contains(searchValue)
                        || e.CompanyName.Contains(searchValue)
                        || e.Country.Contains(searchValue)
                        || e.City.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.MemberId);
                                break;
                            case "email":
                                query = query.OrderBy(e => e.Email);
                                break;
                            case "email desc":
                                query = query.OrderByDescending(e => e.Email);
                                break;
                            case "companyName":
                                query = query.OrderBy(e => e.CompanyName);
                                break;
                            case "companyName desc":
                                query = query.OrderByDescending(e => e.CompanyName);
                                break;
                            case "country":
                                query = query.OrderBy(e => e.Country);
                                break;
                            case "country desc":
                                query = query.OrderByDescending(e => e.Country);
                                break;
                            default:
                                query = query.OrderBy(e => e.MemberId);
                                break;
                        }
                    }

                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    members = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Member>(members, count, pageIndex, pageSize);
        }

        public static Member Login(string email, string password)
        {
            var member = new Member();
            try
            {
                using (var context = new FStoreDBContext())
                {
                    member = context.Members.Where(e => e.Email.Equals(email) && e.Password.Equals(password)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }
        public static Member GetMemberById(int id)
        {
            var member = new Member();
            try
            {
                using (var context = new FStoreDBContext())
                {
                    member = context.Members.Find(id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public static void AddMember(Member member)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    member.MemberId = context.Members.Max(x => x.MemberId) + 1;
                    context.Members.Add(member);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateMember(Member m)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    context.Entry<Member>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteMember(Member m)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    var memberOrder = context.Orders.Where(x => x.MemberId == m.MemberId).FirstOrDefault();
                    if (memberOrder is null)
                    {
                        throw new OperationCanceledException("cannot remove member due to order");
                    }
                    context.Members.Remove(m);
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
