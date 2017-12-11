using System;
using System.ComponentModel.DataAnnotations;

namespace eventy.Models
{
    public class Family : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OldControlNumber { get; set; }
        public string OldFamilyNumber { get; set; }
    }
}