// Remove SchoolId from Lead model as it can be accessed through Branch
namespace LeadManagementSystem.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Foreign Keys
        public int BranchId { get; set; }
        public int LeadSourceId { get; set; }
        public int LeadTypeId { get; set; }
        public int StatusId { get; set; }
        public int OwnerId { get; set; }

        // Navigation Properties
        public Branch Branch { get; set; }
        public LeadSource LeadSource { get; set; }
        public LeadType LeadType { get; set; }
        public Status Status { get; set; }
        public UserModel Owner { get; set; }
    }
}
