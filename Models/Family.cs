using System;

namespace eventy.Models
{
    public class Family : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}