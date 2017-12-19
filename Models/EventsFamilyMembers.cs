using System;

namespace eventy.Models
{
    public class EventsFamilyMembers : BaseEntity
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public long FamilyMemberId { get; set; }
    }
}