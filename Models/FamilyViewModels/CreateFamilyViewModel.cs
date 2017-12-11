using System;
using System.ComponentModel.DataAnnotations;

namespace eventy.Models.FamilyViewModels
{
    public class CreateFamilyViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}