using BusinessObject;
using DataAccess.Daos;
using DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public void AddMember(Member m) => MemberDAO.AddMember(m);

        public void DeleteMember(Member m) => MemberDAO.DeleteMember(m);

        public Member GetMemberById(int id) => MemberDAO.GetMemberById(id);

        public List<Member> GetMembers(string searchValue, int pageIndex, int pageSize, string orderBy) => MemberDAO.GetMembers(searchValue, pageIndex, pageSize, orderBy);

        public void UpdateMember(Member m) => MemberDAO.UpdateMember(m);
        public Member Login(string email, string password) => MemberDAO.Login(email, password);
    }
}
