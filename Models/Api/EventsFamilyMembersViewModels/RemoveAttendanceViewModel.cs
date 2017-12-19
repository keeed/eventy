using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eventy.Models.Api.EventsFamilyMembersViewModels
{
    public class RemoveAttendanceViewModel
    {
        public long EventId { get; set; }
        public long FamilyMemberId { get; set; }
    }
}
