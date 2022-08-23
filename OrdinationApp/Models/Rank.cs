using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdinationApp.Models
{
    public class Rank
    {
        [Key]
        public string Title { get; set; }

        [ForeignKey("RankTitle")]
        public OrdinationBill?  Bill { get; set; }

        [Required]
        public string Gender { get; set; }


        public ICollection<Member> CurrentMembers { get; set; }

        public ICollection<Member> TargetMembers { get; set; }

        public ICollection<PaymentRecord> RankPayments { get; set; }


    }
}
