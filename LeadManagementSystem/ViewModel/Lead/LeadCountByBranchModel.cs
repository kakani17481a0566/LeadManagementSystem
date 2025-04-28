namespace LeadManagementSystem.ViewModel.Lead
{
    public class LeadCountByBranchModel
    {
        public string BranchName { get; set; }
        public int ConvertedCount { get; set; }
        public int TotalCount { get; set; }
        public double SuccessPercentage { get; set; }
    }
}
