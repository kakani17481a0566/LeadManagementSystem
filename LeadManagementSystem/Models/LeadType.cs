using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    [Table("lead_types")]  // Table Name for LeadType
    public class LeadType
    {
        [Column("id")]  // Column Name for Id
        public int Id { get; set; }

        [Column("name")]  // Column Name for Type (in database, it may be called 'name')
        public string Type { get; set; }

        // Navigation Property for Leads (One-to-Many Relationship with LeadEntity)
        public ICollection<LeadEntity> Leads { get; set; }
    }
}
