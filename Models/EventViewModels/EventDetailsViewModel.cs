using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eventy.Models.EventViewModels
{
    public class EventDetailsViewModel
    {
        public Event Event { get; set; }
        public int NumberOfFamiliesRegistered { get; set; }
        public int MaxNumberOfAttendees { get; set; }
        public int TotalNumberOfAttendees { get; set; }
        public List<FamilyMemberDetails> FamilyMembersDetails { get; set; }

        public long NumberOfSeniorCitizensAttending { get; set; }
        public long MaxNumberOfSeniorCitizens { get; set; }
        public long NumberOfKidsAttending { get; set; }
        public long MaxNumberOfKids{ get; set; }
    }

    public class FamilyMemberDetails
    {
        public Family Family { get; set; }
        public FamilyMember FamilyMember { get; set; }
        public bool IsAttending { get; set; }
    }
}
