using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdinationApp.Models
{
    public class PaymentRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PaymentYear { get; set; } = DateTime.Now.Year;

        public string TallyNo { get; set; }


        [ForeignKey("Member")]
        public int MemberId { get; set; }

        [Required]
        public Member Member { get; set; }

        public string RankTitle { get; set; }

        [Required]
        [ForeignKey("RankTitle")]
        public Rank Rank { get; set; }
    }
}
