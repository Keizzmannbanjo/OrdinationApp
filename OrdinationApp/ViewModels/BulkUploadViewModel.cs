using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OrdinationApp.ViewModels
{
    public class BulkUploadViewModel
    {
        [Required(ErrorMessage = "Please, upload an excel file")]
        public IFormFile upload { get; set; }

    }
}
