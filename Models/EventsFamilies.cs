using System;
using System.ComponentModel.DataAnnotations;

namespace eventy.Models
{
    /// <summary>
    /// Many-to-Many relationship for Event Families
    /// </summary>
    public class EventsFamilies : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public long EventId { get; set; }
        public long FamilyId { get; set; }
    }
}