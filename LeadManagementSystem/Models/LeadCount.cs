    using System.Numerics;

    namespace LeadManagementSystem.Models
    {
    public class LeadCount
    {
        public int TotalCount { get; set; }

        public int Open { get; set; }
        public int VisitingSoon { get; set; } //InProcess
        public int SchoolVisited { get; set; }//InProcess
        public int Closed { get; set; }  
        public int NotInterested { get; set; }

        public int Converted { get; set; }
        public int InProcess { get; set; }

        public string Label { get; set; }  
    }

}
