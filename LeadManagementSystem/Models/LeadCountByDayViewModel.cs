namespace LeadManagementSystem.ViewModel.Lead
{
    public class LeadCountByDayViewModel
    {

        public int Day { get; set; }
        public int TotalCount { get; set; }
        public int ConvertedCount { get; set; }
        public int InProgress { get; set; }
        public int NewCount { get; set; }
        public int NonConverted { get; set; }
    }
}
