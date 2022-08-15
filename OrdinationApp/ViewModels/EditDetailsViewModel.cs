using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OrdinationApp.ViewModels
{
    public class EditDetailsViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter a first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please, enter a surname")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please, enter a other name")]
        [Display(Name = "Other Name")]
        public string Othername { get; set; }

        [Required(ErrorMessage = "Please, choose a province")]
        [Display(Name = "Province")]
        public string ProvinceName { get; set; }
        public List<SelectListItem>? ProvinceList { get; set; }

        public List<SelectListItem>? RankList { get; set; }

        [Required(ErrorMessage = "Please, choose a gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please, choose a year for current rank")]
        [Display(Name = "Current Rank Year")]
        public int CurrentRankYear { get; set; }

        [Required(ErrorMessage = "Please, choose a current rank")]
        [Display(Name = "Current Rank Title")]
        public string CurrentRankTitle { get; set; }

        [Required(ErrorMessage = "Please, choose a target rank")]
        [Display(Name = "Target Rank Title")]
        public string TargetRankTitle { get; set; }

        [Required(ErrorMessage = "Please, choose a payment status")]
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
