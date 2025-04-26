namespace LeadManagementSystem.ViewModel.Lead
{
    public class LeadCountByBranchAndSourceViewModel
    {
        public string BranchName { get; set; }
        public string SourceName { get; set; }
        public int LeadCount { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
