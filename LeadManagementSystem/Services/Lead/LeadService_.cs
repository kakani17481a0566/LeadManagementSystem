using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadManagementSystem.Data;
using LeadManagementSystem.ViewModel.Lead;
using LeadManagementSystem.Models;

namespace LeadManagementSystem.Services.Lead
{
    public class LeadService_
    {
        private readonly ApplicationDbContext _context;

        public LeadService_(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Get Lead Count By Status and Total Count for Current Year
        public async Task<(List<LeadCountByStatusViewModel> LeadCountByStatus, int TotalLeadCount)> GetLeadCountByStatusAndTotalCountAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            var leadCountByStatus = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .GroupBy(l => new
                {
                    l.Status.Name,
                    l.DateTime.Month,
                    l.DateTime.Year
                })
                .Select(g => new LeadCountByStatusViewModel
                {
                    StatusName = g.Key.Name,
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    LeadCount = g.Count()
                })
                .OrderBy(r => r.Year)
                .ThenBy(r => r.Month)
                .ToListAsync();

            var totalLeadCount = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .CountAsync();

            return (leadCountByStatus, totalLeadCount);
        }

        // 2. Get Daily Lead Status Summary for Current Month
        public async Task<(List<DailyLeadStatusSummaryViewModel> DailySummary, int TotalLeadCount)> GetDailyLeadStatusSummaryAsync()
        {
            var now = DateTime.UtcNow;
            var currentYear = now.Year;
            var currentMonth = now.Month;

            var summary = await _context.leads
                .Where(l => l.DateTime.Year == currentYear && l.DateTime.Month == currentMonth)
                .GroupBy(l => new
                {
                    l.Status.Name,
                    Day = l.DateTime.Day,
                    Month = l.DateTime.Month,
                    Year = l.DateTime.Year
                })
                .Select(g => new DailyLeadStatusSummaryViewModel
                {
                    StatusName = g.Key.Name,
                    Day = g.Key.Day,
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    LeadCount = g.Count()
                })
                .OrderBy(r => r.Year)
                .ThenBy(r => r.Month)
                .ThenBy(r => r.Day)
                .ToListAsync();

            var totalLeadCount = await _context.leads
                .Where(l => l.DateTime.Year == currentYear && l.DateTime.Month == currentMonth)
                .CountAsync();

            return (summary, totalLeadCount);
        }

        // 3. Get Monthly Lead Status Summary for Current Year
        public async Task<List<MonthlyLeadStatusSummaryViewModel>> GetMonthlyLeadStatusSummaryAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            var result = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .GroupBy(l => new
                {
                    l.Status.Name,
                    Month = l.DateTime.Month,
                    Year = l.DateTime.Year
                })
                .Select(g => new MonthlyLeadStatusSummaryViewModel
                {
                    StatusName = g.Key.Name,
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    LeadCount = g.Count()
                })
                .OrderBy(r => r.Year)
                .ThenBy(r => r.Month)
                .ToListAsync();

            return result;
        }

        // 4. Get Lead Count By Source and Status for Current Year
        public async Task<List<LeadCountBySourceAndStatusViewModel>> GetLeadCountBySourceAndStatusAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            var leadCountBySourceAndStatus = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .GroupBy(l => new
                {
                    l.Status.Name,
                    SourceName = l.LeadSource.Name
                })
                .Select(g => new LeadCountBySourceAndStatusViewModel
                {
                    StatusName = g.Key.Name,
                    SourceName = g.Key.SourceName,
                    LeadCount = g.Count()
                })
                .OrderBy(r => r.SourceName)
                .ToListAsync();

            return leadCountBySourceAndStatus;
        }

        // 5. Get Lead Count By Source and Status for a Given Period (Year & Month)
        public async Task<List<LeadCountBySourceAndStatusByYearViewModel>> GetLeadCountBySourceAndStatusByPeriodAsync(int startYear, int startMonth, int endYear, int endMonth)
        {
            var leadCountBySourceAndStatus = await _context.leads
                .Where(l =>
                    (l.DateTime.Year > startYear || (l.DateTime.Year == startYear && l.DateTime.Month >= startMonth)) &&
                    (l.DateTime.Year < endYear || (l.DateTime.Year == endYear && l.DateTime.Month <= endMonth)))
                .GroupBy(l => new
                {
                    l.Status.Name,
                    SourceName = l.LeadSource.Name,
                    Year = l.DateTime.Year,
                    Month = l.DateTime.Month
                })
                .Select(g => new LeadCountBySourceAndStatusByYearViewModel
                {
                    StatusName = g.Key.Name,
                    SourceName = g.Key.SourceName,
                    LeadCount = g.Count(),
                    Year = g.Key.Year,
                    Month = g.Key.Month
                })
                .OrderBy(r => r.SourceName)
                .ThenBy(r => r.Year)
                .ThenBy(r => r.Month)
                .ToListAsync();

            return leadCountBySourceAndStatus;
        }


        // Service Method to Get Lead Count by Source, Branch, and Month for the Current Year
        public async Task<List<LeadCountBySourceBranchMonthViewModel>> GetLeadCountBySourceBranchMonthAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            // Fetching data using LINQ instead of raw SQL
            var result = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .GroupBy(l => new
                {
                    l.LeadSource.Name,
                    l.Branch.BranchName,
                    Month = l.DateTime.Month
                })
                .Select(g => new LeadCountBySourceBranchMonthViewModel
                {
                    SourceName = g.Key.Name,
                    BranchName = g.Key.BranchName,
                    LeadCount = g.Count(),
                    Year = currentYear,
                    Month = g.Key.Month
                })
                .OrderBy(r => r.BranchName)
                .ThenBy(r => r.Month)
                .ThenBy(r => r.SourceName)
                .ToListAsync();

            return result;
        }


        // Service Method to Get Daily Lead Count by Source, Branch, and Day for Current Month
        public async Task<List<DailyLeadCountBySourceBranchViewModel>> GetDailyLeadCountBySourceAndBranchCurrentMonthAsync()
        {
            var currentYear = DateTime.UtcNow.Year;
            var currentMonth = DateTime.UtcNow.Month;

            // Fetching data using LINQ instead of raw SQL
            var result = await _context.leads
                .Where(l => l.DateTime.Year == currentYear && l.DateTime.Month == currentMonth)
                .GroupBy(l => new
                {
                    l.LeadSource.Name,
                    l.Branch.BranchName,
                    Day = l.DateTime.Day
                })
                .Select(g => new DailyLeadCountBySourceBranchViewModel
                {
                    SourceName = g.Key.Name,
                    BranchName = g.Key.BranchName,
                    LeadCount = g.Count(),
                    Year = currentYear,
                    Month = currentMonth,
                    Day = g.Key.Day
                })
                .OrderBy(r => r.BranchName)
                .ThenBy(r => r.Day)
                .ThenBy(r => r.SourceName)
                .ToListAsync();

            return result;
        }


        // Service Method to Get Lead Count by Branch and Source
        public async Task<List<LeadCountBySourceAndBranchViewModel>> GetLeadCountBySourceAndBranchAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            var result = await _context.leads
                .Where(l => l.Status.Name == "Converted" && l.DateTime.Year == currentYear)
                .GroupBy(l => l.Branch.BranchName)
                .Select(g => new LeadCountBySourceAndBranchViewModel
                {
                    BranchName = g.Key,
                    ConvertedCount = g.Count()
                })
                .OrderBy(r => r.BranchName)
                .ToListAsync();

            return result;
        }




        public async Task<List<LeadCountByBranchAndSourceViewModel>> GetLeadCountByDateRangeAsync(
            int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            // Create DateTime objects for start and end date
            var startDate = new DateTime(startYear, startMonth, startDay);
            var endDate = new DateTime(endYear, endMonth, endDay).AddDays(1); // Add one day to include the full last day

            // Fetch all leads from the database
            var allLeads = await _context.leads
                .Include(l => l.Branch)
                .Include(l => l.LeadSource)
                .ToListAsync();

            // Initialize a list to hold the result after filtering and processing
            var leadCountResult = new List<LeadCountByBranchAndSourceViewModel>();

            // Process leads to count based on date range
            foreach (var lead in allLeads)
            {
                if (lead.DateTime >= startDate && lead.DateTime < endDate)
                {
                    var day = lead.DateTime.Day;
                    var existingRecord = leadCountResult.Find(r => r.BranchName == lead.Branch.BranchName &&
                                                                  r.SourceName == lead.LeadSource.Name &&
                                                                  r.Day == day);

                    if (existingRecord != null)
                    {
                        // If record exists, increment the lead count
                        existingRecord.LeadCount++;
                    }
                    else
                    {
                        // If no existing record, create a new entry
                        leadCountResult.Add(new LeadCountByBranchAndSourceViewModel
                        {
                            BranchName = lead.Branch.BranchName,
                            SourceName = lead.LeadSource.Name,
                            LeadCount = 1,
                            Day = day
                        });
                    }
                }
            }

            return leadCountResult;
        }
        public async Task<List<LeadCountByBranchAndSourceViewModel>> DateTimeGetLeadCountByDateRangeAsync(
     int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            // Ensure the DateTime values are explicitly set to UTC
            var startDate = new DateTime(startYear, startMonth, startDay, 0, 0, 0, DateTimeKind.Utc);
            var endDate = new DateTime(endYear, endMonth, endDay, 0, 0, 0, DateTimeKind.Utc).AddDays(1); // Include entire end day

            // Fetch leads within the date range
            var allLeads = await _context.leads
                .Include(l => l.Branch)
                .Include(l => l.LeadSource)
                .Where(l => l.DateTime >= startDate && l.DateTime < endDate)
                .ToListAsync();

            // Return raw leads (or group later)
            return allLeads.Select(l => new LeadCountByBranchAndSourceViewModel
            {
                Year = l.DateTime.Year,
                Month = l.DateTime.Month,
                Day = l.DateTime.Day,
                BranchName = l.Branch?.BranchName ?? "Unknown",
                SourceName = l.LeadSource?.Name ?? "Unknown",
                LeadCount = 1 // temporary
            }).ToList();
        }


        public async Task<List<LeadCount>> GetLeadCountByStatusAndMonthAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            var leadCountByStatusAndMonth = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .GroupBy(l => new
                {
                    l.Status.Name,
                    Month = l.DateTime.Month,
                    Year = l.DateTime.Year
                })
                .Select(g => new LeadCountByStatusViewModel
                {
                    StatusName = g.Key.Name,
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    LeadCount = g.Count()
                })
                .OrderBy(r => r.Year)
                .ThenBy(r => r.Month)
                .ToListAsync();

            List<LeadCount> LeadCountList = new List<LeadCount>();
            string[] MonthList = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            LeadCount newleadCount = new LeadCount();

            int MonthFlag = 0;

            foreach (var item in leadCountByStatusAndMonth)
            {
                Console.WriteLine(item.Month);

                if (item.Month != MonthFlag)
                {
                    if (MonthFlag != 0) { LeadCountList.Add(newleadCount); }

                    MonthFlag = item.Month;

                    newleadCount = new LeadCount();
                        newleadCount.Label = MonthList[item.Month-1]  ;
                        
                    
                    


                 }

                newleadCount.TotalCount += item.LeadCount; 

                switch (item.StatusName)
                {
                    case "Converted":
                        newleadCount.ConvertedCount += item.LeadCount;
                        break;
                    case "InProgress":
                        newleadCount.InProgress += item.LeadCount;
                        break;
                    case "New":
                        newleadCount.NewCount += item.LeadCount;
                        break;
                    case "NonConverted":
                        newleadCount.NonConverted += item.LeadCount;
                        break;

                }

               
            }

            if (MonthFlag != 0) { LeadCountList.Add(newleadCount); }




            return LeadCountList;
        }


        public async Task<List<LeadCountByDayViewModel>> GetLeadCountByDayAsync()
        {
            var currentDate = DateTime.UtcNow;
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;

            var leadCountByDay = await _context.leads
                .Where(l => l.DateTime.Year == currentYear && l.DateTime.Month == currentMonth)
                .GroupBy(l => new { l.DateTime.Day, l.Status.Name })
                .Select(g => new LeadCountByDayViewModel
                {
                    Day = g.Key.Day,  
                    TotalCount = g.Count(),
                    ConvertedCount = g.Key.Name == "Converted" ? g.Count() : 0,
                    InProgress = g.Key.Name == "InProgress" ? g.Count() : 0,
                    NewCount = g.Key.Name == "New" ? g.Count() : 0,
                    NonConverted = g.Key.Name == "NonConverted" ? g.Count() : 0
                })
                .OrderBy(r => r.Day)
                .ToListAsync();

            return leadCountByDay;
        }



        public async Task<List<LeadCountBySourceViewModel>> GetLeadCountBySourceAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            var result = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .GroupBy(l => l.LeadSource.Name)
                .Select(g => new LeadCountBySourceViewModel
                {
                    SourceName = g.Key,
                    TotalLeads = g.Count()
                })
                .OrderBy(x => x.SourceName)
                .ToListAsync();

            return result;
        }


        public async Task<List<LeadCountByBranchModel>> GetLeadCountByBranchAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            var leadCounts = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)  // Filter by the current year using Year property
                .GroupBy(l => new { l.Branch.BranchName, l.BranchId })  // Group by branch
                .Select(g => new LeadCountByBranchModel
                {
                    BranchName = g.Key.BranchName,
                    ConvertedCount = g.Count(l => l.Status.Name == "Converted"),  // Count converted leads
                    TotalCount = g.Count(),  // Count all leads
                })
                .ToListAsync();

            // Calculate Success Percentage
            foreach (var item in leadCounts)
            {
                if (item.TotalCount > 0)
                {
                    item.SuccessPercentage = Math.Round((item.ConvertedCount * 100.0) / item.TotalCount, 2);

                }
                else
                {
                    item.SuccessPercentage = 0;
                }
            }

            return leadCounts.OrderBy(x => x.BranchName).ToList();  // Sort by branch name
        }



    }
}

