using System.ComponentModel.DataAnnotations;

namespace OrdinationApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="This field is required")]
        public string UserName { get; set; }

        
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
