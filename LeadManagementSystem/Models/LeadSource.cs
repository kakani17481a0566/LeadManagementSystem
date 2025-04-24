namespace LeadManagementSystem.Models
{
    public class LeadSource
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public ICollection<Lead> Leads { get; set; }
    }

}
