using System;
using System.Collections.Generic;

namespace eventy.Models.FamilyViewModels
{
    public class FamilyDetailsViewModel
    {
        public Family Family { get; set; }
        public List<FamilyMember> FamilyMembers { get; set; }
    }
}