//using Microsoft.EntityFrameworkCore;
//using OrdinationApp.Data;
//using OrdinationApp.Models;

//namespace OrdinationApp.Services.ModelServices
//{
//    public class BranchServices : IBranchServices
//    {
//        private readonly ApplicationDbContext _db;
//        public BranchServices(ApplicationDbContext db)
//        {
//            _db = db;
//        }

//        public void AddBranch(string province, string branch)
//        {
//            var model = new Branch { ProvinceName = province, Name = branch };
//            _db.Branches.Add(model);
//            _db.SaveChanges();
//        }

//        public IEnumerable<Branch> GetBranches()
//        {
//            return _db.Branches.Include(b => b.Province).Include(b => b.Members);
//        }
//    }
//}
