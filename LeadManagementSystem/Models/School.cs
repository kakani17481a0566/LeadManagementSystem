namespace LeadManagementSystem.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public ICollection<Branch> Branches { get; set; }
        public ICollection<Lead> Leads { get; set; }
    }

}