using Microsoft.AspNetCore.Mvc.Rendering;
using OrdinationApp.Models;
using System.ComponentModel.DataAnnotations;

namespace OrdinationApp.ViewModels
{
    public class ManageProvincesViewModel
    {
        [Required(ErrorMessage ="Please, select the CMC for province")]
        public string CmcName { get; set; }

        [Required(ErrorMessage ="Please, enter the name of the new province")]
        public string NewProvinceName { get; set; }

        public List<SelectListItem>? CMCs { get; set; }
        public IEnumerable<Province>? Provinces { get; set; }


    }
}
