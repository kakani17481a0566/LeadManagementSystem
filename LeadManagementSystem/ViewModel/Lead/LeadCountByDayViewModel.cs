namespace LeadManagementSystem.ViewModel.Lead
{
    public class LeadCountByDayViewModel
    {
        public int Day { get; set; }
        public int TotalCount { get; set; }

        public int Open { get; set; }
        public int VisitingSoon { get; set; }
        public int SchoolVisited { get; set; }
        public int Closed { get; set; }
        public int NotInterested { get; set; }
    }


}
