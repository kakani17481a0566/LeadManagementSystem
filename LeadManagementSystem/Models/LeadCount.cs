using System.Numerics;

namespace LeadManagementSystem.Models
{
    public class LeadCount
    {
        public int TotalCount { get; set; }

        public int ConvertedCount { get; set; }

        public int NewCount { get; set; }


        public int NonConverted{ get; set; }

        public int InProgress { get; set; }

        public string Label { get; set; }
    }
}
