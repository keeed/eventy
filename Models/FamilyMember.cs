using System;
using System.ComponentModel.DataAnnotations;

namespace eventy.Models
{
    public class FamilyMember : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Family Id
        /// [Optional]
        /// </summary>
        /// <returns></returns>
        public long FamilyId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsHeadOfFamily { get; set; }
    }
}