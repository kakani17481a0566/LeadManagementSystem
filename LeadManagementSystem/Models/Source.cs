using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    [Table("sources")]  // Maps to the "sources" table in the database
    public class Source
    {
        [Column("id")]  // Maps to the "id" column
        public int Id { get; set; }

        [Column("name")]  // Maps to the "name" column
        public string Name { get; set; }

        // Navigation Property for Leads (One-to-Many Relationship with LeadEntity)
        public ICollection<LeadEntity> Leads { get; set; }
    }
}
