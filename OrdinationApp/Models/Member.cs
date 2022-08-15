using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdinationApp.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        public string MemberShipId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Othername { get; set; }

        [Required]
        public int CurrentRankYear { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Status { get; set; } = "pending";

        [ForeignKey("PaymentRecord")]
        public int? PaymentRecordId { get; set; }

        public PaymentRecord? PaymentRecord { get; set; }

        public string ProvinceName { get; set; }

        [Required]
        [ForeignKey("ProvinceName")]
        public Province Province { get; set; }

        public string CurrentRankTitle { get; set; }

        public string TargetRankTitle { get; set; }

        public int OrdinationYear { get; set; } = DateTime.Now.Year;

        [ForeignKey("CurrentRankTitle")]
        public Rank CurrentRank { get; set; }

        [ForeignKey("TargetRankTitle")]
        public Rank TargetRank { get; set; }

    }
}
