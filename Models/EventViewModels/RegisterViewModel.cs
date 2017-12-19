using eventy.Models.GenericViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace eventy.Models.EventViewModels
{
    public class RegisterViewModel
    {
        public Family Family { get; set; }
        public string FamilyId { get; set; }

        public string EventId { get; set; }
        public List<SelectListItem> Events { get; set; }
    }
}