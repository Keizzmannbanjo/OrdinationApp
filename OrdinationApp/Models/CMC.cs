using System.ComponentModel.DataAnnotations;

namespace OrdinationApp.Models
{
    public class CMC
    {
        [Key]
        public string Name { get; set; }
        public ICollection<Province> Provinces { get; set; }
    }
}
