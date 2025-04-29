using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    public class LeadEntity
    {
        [Key]
        [Column("id")]  // Primary key of the leads table
        public int Id { get; set; }

        [Column("lead_name")]  // Name of the lead
        public string Name { get; set; }

        [Column("contact_number")]  // Contact number for the lead
        public string ContactNumber { get; set; }

        [Column("lead_source_id")]  // Foreign key to source table
        public int LeadSourceId { get; set; }

        [Column("branch_id")]  // Foreign key to branch
        public int BranchId { get; set; }

        [Column("lead_type_id")]  // Foreign key to lead type
        public int LeadTypeId { get; set; }

        [Column("lead_date_time")]  // When the lead was created
        public DateTime DateTime { get; set; }

        [Column("is_converted")]  // Indicates if the lead is converted
        public bool Converted { get; set; }

        [Column("sales_person_id")]  // Foreign key to SalesPerson
        public int SalesPersonId { get; set; }  // Nullable if not always assigned

        [Column("lead_list_id")]  // Foreign key to lead list
        public int LeadListId { get; set; }

        [Column("status_id")]  // Foreign key to lead status
        public int StatusId { get; set; }

        [Column("owner_id")]  // Foreign key to user (owner)
        public int OwnerId { get; set; }

        [Column("school_id")]  // Foreign key to school
        public int SchoolId { get; set; }

        // --- Navigation Properties ---
        public LeadType LeadType { get; set; }         // Navigation property for LeadType
        public Source LeadSource { get; set; }         // Navigation property for LeadSource
        public Status Status { get; set; }             // Navigation property for Status
        public LeadList LeadList { get; set; }         // Navigation property for LeadList
        public UserModel Owner { get; set; }           // Navigation property for Owner (User)
        public Branch Branch { get; set; }             // Navigation property for Branch
        public School School { get; set; }             // Navigation property for School
        public SalesPerson SalesPerson { get; set; }   // Navigation property for SalesPerson
    }
}
