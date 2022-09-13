using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OrdinationApp.ViewModels
{
    public class PasswordRecoveryViewModel
    {
        [Required]
        public string Username { get; set; }


        [Required]
        public string Email { get; set; }


    }
}
