namespace LeadManagementSystem.Models
{
    public class LeadType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        // Navigation Property
        public ICollection<Lead> Leads { get; set; }
    }

}