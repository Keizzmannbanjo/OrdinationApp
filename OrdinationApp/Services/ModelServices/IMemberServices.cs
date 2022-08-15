using OrdinationApp.Models;

namespace OrdinationApp.Services.ModelServices
{
    public interface IMemberServices
    {
        IEnumerable<Member> GetMembers();

        Member GetMember(int id);

        bool AddMember(Member member);
        void UpdateMember(Member model);
    }
}
