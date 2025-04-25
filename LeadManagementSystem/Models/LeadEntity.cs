using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    public class LeadEntity
    {
        [Key]
        [Column("id")]  // Maps to the "id" column in the "leads" table
        public int Id { get; set; }

        [Column("lead_name")]  // Maps to the "lead_name" column
        public string Name { get; set; }

        [Column("contact_number")]  // Maps to the "contact_number" column
        public string ContactNumber { get; set; }

        [Column("lead_source_id")]  // Maps to the "lead_source_id" column
        public int LeadSourceId { get; set; }

        [Column("branch_id")]  // Maps to the "branch_id" column
        public int BranchId { get; set; }

        [Column("lead_type_id")]  // Maps to the "lead_type_id" column
        public int LeadTypeId { get; set; }

        [Column("lead_date_time")]  // Maps to the "lead_date_time" column
        public DateTime DateTime { get; set; }

        [Column("is_converted")]  // Maps to the "is_converted" column
        public bool Converted { get; set; }

        [Column("sales_person")]  // Maps to the "sales_person" column
        public string SalesPerson { get; set; }

        [Column("lead_list_id")]  // Maps to the "lead_list_id" column
        public int LeadListId { get; set; }

        [Column("status_id")]  // Maps to the "status_id" column
        public int StatusId { get; set; }

        [Column("owner_id")]  // Maps to the "owner_id" column
        public int OwnerId { get; set; }

        [Column("school_id")]  // Maps to the "school_id" column
        public int SchoolId { get; set; }

        // --- Navigation Properties ---
        public LeadType LeadType { get; set; }  // Navigation property for LeadType
        public Source LeadSource { get; set; }  // Navigation property for LeadSource
        public Status Status { get; set; }     // Navigation property for Status
        public LeadList LeadList { get; set; } // Navigation property for LeadList
        public UserModel Owner { get; set; }    // Navigation property for Owner (User)
        public Branch Branch { get; set; }      // Navigation property for Branch
        public School School { get; set; }      // Navigation property for School
    }
}
