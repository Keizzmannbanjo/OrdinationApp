using System.ComponentModel.DataAnnotations;

namespace OrdinationApp.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Password mismatch, try again...")]
        public string  ConfirmNewPassword { get; set; }

    }
}
