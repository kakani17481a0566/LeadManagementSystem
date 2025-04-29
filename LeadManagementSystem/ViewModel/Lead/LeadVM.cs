namespace LeadManagementSystem.ViewModel.Lead
{
    public class LeadVM
    {
        public string LeadName { get; set; }
        public string ContactNumber { get; set; }
        public int LeadSourceId { get; set; }
        public string LeadSourceName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public int LeadTypeId { get; set; }
        public string LeadTypeName { get; set; }
        public DateTime DateTime { get; set; }
        public bool Converted { get; set; }
        public int SalesPersonId { get; set; }
        public int LeadListId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
    }
}
