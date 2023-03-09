using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrdinationApp.ViewModels
{
    public class EditUserRoleViewModel
    {
        public string userId { get; set; }
        public string role { get; set; }

        public List<SelectListItem>? roleList { get; set; }
    }
}
