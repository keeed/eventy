using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using eventy.Models.GenericViewModels;

namespace eventy.Models.FamilyMemberViewModels
{
    public class CreateFamilyMemberViewModel
    {
        public long FamilyId { get; set; }
        [Required]
        public string FullName { get; set; }
        public ListItemViewModel Genders { get; set; }
        [Required]
        public string SelectedGender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "MM/dd/yyyy")]
        public DateTime Birthday { get; set; }
        public ListItemViewModel HeadOfFamily { get; set; }
        [Required]
        public string IsHeadOfFamily { get; set; }
    }
}