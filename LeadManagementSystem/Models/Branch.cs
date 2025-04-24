namespace LeadManagementSystem.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign Key
        public int SchoolId { get; set; }

        // Navigation Property
        public School School { get; set; }
        public ICollection<Lead> Leads { get; set; }
    }

}