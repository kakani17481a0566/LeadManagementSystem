using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    [Table("schools")]  // This maps the class to the 'schools' table in the database.
    public class School
    {
        [Column("id")]  // This maps the 'Id' property to the 'id' column in the 'schools' table.
        public int Id { get; set; }

        [Column("name")]  // This maps the 'Name' property to the 'name' column in the 'schools' table.
        public string Name { get; set; }

        // Navigation Property for Branches (One-to-Many Relationship with Branch)
        public ICollection<Branch> Branches { get; set; }

        // Navigation Property for Leads (One-to-Many Relationship with LeadEntity)
        public ICollection<LeadEntity> Leads { get; set; }
    }
}
