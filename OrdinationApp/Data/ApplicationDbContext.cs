using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrdinationApp.Models;

namespace OrdinationApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Province> Provinces { get; set; }

        public DbSet<TrackerUser> TrackerUsers { get; set; }

        public DbSet<CMC> CMCs { get; set; }

        public DbSet<Rank> Ranks { get; set; }

        public DbSet<PaymentRecord> PaymentRecords { get; set; }

        public DbSet<OrdinationBill> OrdinationBills { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var roles = new IdentityRole[]
            {
                new IdentityRole{Id=Guid.NewGuid().ToString(), Name="Coder", NormalizedName="Coder".ToUpper()}, new IdentityRole{Id=Guid.NewGuid().ToString(), Name="Admin",NormalizedName="Admin".ToUpper()}
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            modelBuilder.Entity<Member>().HasOne(m => m.CurrentRank).WithMany(r => r.CurrentMembers).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Member>().HasOne(m => m.TargetRank).WithMany(r => r.TargetMembers).OnDelete(DeleteBehavior.NoAction);

            var cmcs = new CMC[] { new CMC { Name = "CMC 1" }, new CMC { Name = "CMC 2" }, new CMC { Name = "CMC 3" }, new CMC { Name = "CMC 4" }, new CMC { Name = "CMC 5" }, new CMC { Name = "CMC 6" }, new CMC { Name = "CMC 7" }, new CMC { Name = "CMC 8" }, new CMC { Name = "CMC 9" }, new CMC { Name = "CMC 10" }, new CMC { Name = "CMC 11" }, new CMC { Name = "CMC 12" } };

            modelBuilder.Entity<CMC>().HasData(cmcs);


            var ranks = new Rank[]
            {
                new Rank
                {
                    Title="Brother",Gender="Male"
                },new Rank
                {
                    Title="Sister",Gender="Female"
                },
                new Rank
                {
                    Title="Army Of Salvation",Gender="Both"
                }, new Rank
                {
                    Title="Army Of Christ", Gender="Both"
                },new Rank
                {
                    Title="Aladura",Gender="Male"
                },
                new Rank
                {
                    Title="Lady Aladura",Gender="Female"
                }, new Rank
                {
                    Title="Leader",Gender="Male"
                }, new Rank
                {
                    Title="Lady Leader",Gender="Female"
                }, new Rank
                {
                    Title="Rabbi",Gender="Male"
                }, new Rank
                {
                    Title="Dorcas",Gender="Female"
                },  new Rank
                {
                    Title="Pastor",Gender="Male"
                }, new Rank
                {
                    Title="Deborah",Gender="Female"
                }, new Rank
                {
                    Title="Evangelist",Gender="Male"
                }, new Rank
                {
                    Title="Mary",Gender="Female"
                }, new Rank
                {
                    Title="Apostle",Gender="Male"
                }, new Rank
                {
                    Title="Prophetess",Gender="Female"
                }, new Rank
                {
                    Title="Supervising Apostle",Gender="Male"
                }, new Rank
                {
                    Title="Mother In Israel",Gender="Female"
                }, new Rank
                {
                    Title="Senior Apostle",Gender="Male"
                }, new Rank
                {
                    Title="Senior Mother In Israel",Gender="Female"
                }, new Rank
                {
                    Title="Special Senior Apostle",Gender="Male"
                }, new Rank
                {
                    Title="Apostle General",Gender="Male"
                },
                new Rank
                {
                    Title="Supervising Apostle General",Gender="Male"
                }
            };

            modelBuilder.Entity<Rank>().HasData(ranks);


            //var members = new Member[]
            //{
            //    new Member{Id=1, FirstName="Michael",Surname="Banjo", Othername="Kolawole", CurrentRankYear=2010,Gender="Male",Status="pending", OrdinationYear=2022, ProvinceName="Ebute-Metta", CurrentRankTitle="Army Of Salvation", TargetRankTitle="Aladura"}, new Member{Id=2, FirstName="Yetunde",Surname="Banjo", Othername="Victoria", CurrentRankYear= 2012, Gender="Female",Status="pending", OrdinationYear=2022, ProvinceName="Ebute-Metta", CurrentRankTitle="Army Of Salvation", TargetRankTitle="Lady Aladura"},
            //};

            //modelBuilder.Entity<Member>().HasData(members);
        }
    }
}
