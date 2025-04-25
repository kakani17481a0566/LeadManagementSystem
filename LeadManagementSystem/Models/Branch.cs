using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    [Table("branches")]  // Table Name for Branch
    public class Branch
    {
        [Column("id")]  // Column Name for Id
        public int Id { get; set; }

        [Column("branch_name")]  // Column Name for branch_name
        public string Name { get; set; }

        [Column("school_id")]  // Column Name for school_id (Foreign Key to School)
        public int SchoolId { get; set; }

        // Navigation Property for School (One-to-Many Relationship with School)
        public School School { get; set; }

        // Navigation Property for Leads (One-to-Many Relationship with LeadEntity)
        public ICollection<LeadEntity> Leads { get; set; }
    }
}
