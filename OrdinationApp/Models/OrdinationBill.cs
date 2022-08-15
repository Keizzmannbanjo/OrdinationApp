using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdinationApp.Models
{
    public class OrdinationBill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName="money")]
        public decimal OrdinationFee { get; set; }

        public string RankTitle { get; set; }

        [Required]
        public Rank Rank { get; set; }

        [Column(TypeName = "money")]
        public decimal TrainingFee { get; set; }

        [Column(TypeName = "money")]
        public decimal WoodenStaffPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal IronStaffPrice { get; set; }

    }
}
