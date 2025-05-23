﻿using Microsoft.EntityFrameworkCore;

namespace LeadManagementSystem.ViewModel.Lead
{
    [Keyless]
    public class DailyLeadStatusSummaryViewModel
    {
        public int LeadCount { get; set; }
        public string StatusName { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
