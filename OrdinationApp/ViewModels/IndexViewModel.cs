using Microsoft.AspNetCore.Mvc.Rendering;
using OrdinationApp.Models;

namespace OrdinationApp.ViewModels
{
    public class IndexViewModel
    {
        public List<SelectListItem> provinceList { get; set; }

        public List<SelectListItem> rankList { get; set; }

        public IEnumerable<Member> members { get; set; }

    }
}
