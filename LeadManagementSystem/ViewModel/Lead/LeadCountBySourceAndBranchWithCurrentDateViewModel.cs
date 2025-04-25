namespace LeadManagementSystem.ViewModel.Lead
{

    public class LeadCountBySourceAndBranchWithCurrentDateViewModel
    {
        public string SourceName { get; set; }
        public string BranchName { get; set; }
        public int LeadCount { get; set; }
        public int CurrentYear { get; set; }
        public int CurrentMonth { get; set; }
    }


}
