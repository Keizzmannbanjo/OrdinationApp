using System.ComponentModel.DataAnnotations;

namespace OrdinationApp.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Password mismatch, try again...")]
        public string  ConfirmNewPassword { get; set; }

    }
}
