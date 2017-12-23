using System;
using System.ComponentModel.DataAnnotations;

namespace eventy.Models
{
    public class Event : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string EventName { get; set; }
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }
        public int MaxNumberOfFamilies { get; set; }
    }
}