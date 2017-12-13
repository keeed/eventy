using System;
using System.ComponentModel.DataAnnotations;

namespace eventy.Models.EventViewModels
{
    public class CreateEventViewModel
    {
        [Required]
        public string EventName { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public int MaxNumberOfFamilies { get; set; }
    }
}