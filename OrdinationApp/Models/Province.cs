using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdinationApp.Models
{
    public class Province
    {
        [Key]
        public string Name { get; set; }


        public string CmcName { get; set; }

        [Required]
        [ForeignKey("CmcName")]
        public CMC CMC { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}
