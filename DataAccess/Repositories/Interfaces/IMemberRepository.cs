using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        List<Member> GetMembers(string searchValue, int pageIndex, int pageSize, string orderBy);

        Member GetMemberById(int id);

        void AddMember(Member m);

        void UpdateMember(Member m);

        void DeleteMember(Member m);
    }
}
