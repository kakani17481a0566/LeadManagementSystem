using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    [Table("statuses")]  // Table Name for Status
    public class Status
    {
        [Column("id")]  // Column Name for Id
        public int Id { get; set; }

        [Column("status_name")]  // Column Name for Name
        public string Name { get; set; }

        // Navigation Property for Leads (One-to-Many Relationship with LeadEntity)
        public ICollection<LeadEntity> Leads { get; set; }
    }
}
