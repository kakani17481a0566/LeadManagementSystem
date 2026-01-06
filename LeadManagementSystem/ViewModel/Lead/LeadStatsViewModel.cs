using System.Collections.Generic;

namespace LeadManagementSystem.ViewModel.Lead
{
    public class LeadStatsViewModel
    {
        public LeadTotalsViewModel LeadTotals { get; set; }
        public ChartDataViewModel Yearly { get; set; }
        public ChartDataViewModel Monthly { get; set; }
    }

    public class LeadTotalsViewModel
    {
        public int TotalLeads { get; set; }
        public int ConvertedLeads { get; set; }
        public int InProcessLeads { get; set; }
        public int NonConverted { get; set; }
    }

    public class ChartDataViewModel
    {
        public List<SeriesData> Series { get; set; }
        public List<object> Categories { get; set; }
    }

    public class SeriesData
    {
        public string Name { get; set; }
        public List<int> Data { get; set; }
    }
}
