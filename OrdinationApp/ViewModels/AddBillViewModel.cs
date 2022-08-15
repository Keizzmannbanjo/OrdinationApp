﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OrdinationApp.ViewModels
{
    public class AddBillViewModel
    {
        [Required(ErrorMessage ="Please, specify a rank")]
        public string RankTitle { get; set; }

        [Required(ErrorMessage ="Please, enter an ordination fee")]
        [Display(Name = "Ordination Fee")]
        public decimal OrdinationFee { get; set; }

        [Required(ErrorMessage = "Please, enter a training fee")]
        [Display(Name = "Training Fee")]
        public decimal TrainingFee { get; set; }

        [Required(ErrorMessage = "Please, enter a wooden staff fee")]
        [Display(Name = "Wooden Staff Fee")]
        public decimal WoodenStaffPrice { get; set; }

        [Required(ErrorMessage = "Please, enter an iron staff fee")]
        [Display(Name = "Iron Staff Fee")]
        public decimal IronStaffPrice { get; set; }

        public List<SelectListItem>? RankList{ get; set; }
    }
}
