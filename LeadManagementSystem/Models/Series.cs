namespace LeadManagementSystem.Models
{
    public class Series
    {
        public string Name { get; set; }
        public int[] Data { get; set; }
        public Series()
        {
            this.Name = "";
            this.Data = new int[0];
        }
    }


    public class Period
    {
        public List<Series> series { get; set; }
        public string[] Categories { get; set; }

        public Period()
        {
            this.series = new List<Series>();
            this.Categories = new string[0];
        }
    }


    public class LeadTotals
    {
        public int TotalLeads { get; set; }
        public int ConvertedLeads { get; set; }
        public int InProcessLeads { get; set; }
        public int NonConverted { get; set; }

        public LeadTotals()
        {
            this.TotalLeads = 0;
            this.ConvertedLeads = 0;
            this.InProcessLeads = 0;
            this.NonConverted = 0;

        }
    }



    public class LeadStats()
    { 
        public Period yearly { get; set; }
        public Period monthly { get; set; }
        public LeadTotals leadTotals { get; set; }
        public LeadTotals MonthlyTotalLeads{ get; set; }


    }


}