using Microsoft.EntityFrameworkCore;
using OrdinationApp.Data;
using OrdinationApp.Models;

namespace OrdinationApp.Services.ModelServices
{
    public class MemberServices : IMemberServices
    {
        private readonly ApplicationDbContext _db;
        public MemberServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Member> GetMembers()
        {
            return _db.Members.Include(s => s.Province).Include(s => s.CurrentRank).Include(s => s.TargetRank);
        }

        public bool AddMember(Member member)
        {
            var checkMember = _db.Members.Any(m => (m.Surname == member.Surname) && (m.FirstName == member.FirstName) && (m.Othername == member.Othername) && (m.ProvinceName == member.ProvinceName) && (m.CurrentRankTitle == member.CurrentRankTitle) && (m.OrdinationYear == member.OrdinationYear));
            if (!checkMember)
            {
                _db.Members.Add(member);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public Member GetMember(int id)
        {
            return _db.Members.First(m => m.Id == id);
        }

        public void UpdateMember(Member member)
        {
            _db.Members.Update(member);
            _db.SaveChanges();
        }
    }
}
