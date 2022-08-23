using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OrdinationApp.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name ="Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password does not match, try again...")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Rank")]
        public string RankTitle { get; set; }

        [Required]
        [Display(Name ="Province")]
        public string ProvinceName { get; set; }

        public List<SelectListItem>? RankList { get; set; }

        public List<SelectListItem>? ProvinceList { get; set; }
        public List<SelectListItem>? RoleList { get; set; }
    }
}
