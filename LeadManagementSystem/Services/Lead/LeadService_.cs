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


        public async Task<List<LeadCount>> GetLeadCountByStatusAndMonthAsync(int? year = null)
        {
            var currentYear = year ?? DateTime.UtcNow.Year;

            var leadCountByStatusAndMonth = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .GroupBy(l => new
                {
                    l.Status.Name,
                    l.Converted,
                    Month = l.DateTime.Month,
                    Year = l.DateTime.Year
                })
                .Select(g => new LeadCountByStatusViewModel
                {
                    StatusName = g.Key.Name,
                    IsConverted = g.Key.Converted,
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
                if (item.Month != MonthFlag)
                {
                    if (MonthFlag != 0) { LeadCountList.Add(newleadCount); }

                    MonthFlag = item.Month;

                    newleadCount = new LeadCount();
                    if (item.Month >= 1 && item.Month <= 12)
                    {
                        newleadCount.Label = MonthList[item.Month - 1];
                    }
                    else
                    {
                        newleadCount.Label = "UNK";
                    }
                }

                newleadCount.TotalCount += item.LeadCount; 

                var status = item.StatusName?.Trim();
                
                // Handle explicit "Converted" flag priority if needed, or stick to status logic
                if (item.IsConverted)
                {
                     newleadCount.ConvertedCount += item.LeadCount;
                }
                else 
                {
                     // Non-converted logic based on status name
                     if (string.Equals(status, "New", StringComparison.OrdinalIgnoreCase) || 
                         string.Equals(status, "Open", StringComparison.OrdinalIgnoreCase))
                     {
                         newleadCount.NewCount += item.LeadCount;
                     }
                     else if (string.Equals(status, "InProgress", StringComparison.OrdinalIgnoreCase) || 
                              string.Equals(status, "InProcess", StringComparison.OrdinalIgnoreCase) ||
                              string.Equals(status, "Visiting Soon", StringComparison.OrdinalIgnoreCase) ||
                              string.Equals(status, "School Visited", StringComparison.OrdinalIgnoreCase))
                     {
                         newleadCount.InProgress += item.LeadCount;
                     }
                     else if (string.Equals(status, "NonConverted", StringComparison.OrdinalIgnoreCase) || 
                              string.Equals(status, "Not Interested", StringComparison.OrdinalIgnoreCase) ||
                              string.Equals(status, "Waste Lead", StringComparison.OrdinalIgnoreCase))
                     {
                         newleadCount.NonConverted += item.LeadCount;
                     }
                     else if (string.Equals(status, "Closed", StringComparison.OrdinalIgnoreCase)) 
                     {
                         // Closed but IsConverted is false -> Likely Lost/NonConverted
                         newleadCount.NonConverted += item.LeadCount;
                     }
                }
            }

            if (MonthFlag != 0) { LeadCountList.Add(newleadCount); }




            return LeadCountList;
        }


        public async Task<List<LeadCountByDayViewModel>> GetLeadCountByDayAsync(int? year = null, int? month = null)
        {
            var currentDate = DateTime.UtcNow;
            var currentMonth = month ?? currentDate.Month;
            var currentYear = year ?? currentDate.Year;

            // 1. Fetch raw flattened data
            var rawData = await _context.leads
                .Where(l => l.DateTime.Year == currentYear && l.DateTime.Month == currentMonth)
                .GroupBy(l => new { l.DateTime.Day, StatusName = l.Status != null ? l.Status.Name : "Unknown" })
                .Select(g => new
                {
                    Day = g.Key.Day,
                    StatusName = g.Key.StatusName,
                    Count = g.Count()
                })
                .ToListAsync();

            // 2. Aggregate in memory to ensure one record per day
            var leadCountByDay = rawData
                .GroupBy(x => x.Day)
                .Select(g => new LeadCountByDayViewModel
                {
                    Day = g.Key,
                    TotalCount = g.Sum(x => x.Count),
                    ConvertedCount = g.Where(x => x.StatusName == "Converted").Sum(x => x.Count),
                    InProgress = g.Where(x => x.StatusName == "InProgress").Sum(x => x.Count),
                    NewCount = g.Where(x => x.StatusName == "New").Sum(x => x.Count),
                    NonConverted = g.Where(x => x.StatusName == "NonConverted").Sum(x => x.Count)
                })
                .OrderBy(r => r.Day)
                .ToList();

            return leadCountByDay;
        }



        public async Task<List<LeadCountBySourceViewModel>> GetLeadCountBySourceAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            // 1. Get all sources
            var allSources = await _context.sources.ToListAsync();

            // 2. Get Lead counts grouped by Source for current year
            var leadCountsGrouped = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .GroupBy(l => l.LeadSourceId)
                .Select(g => new
                {
                    SourceId = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            // 3. Left Join
            var result = allSources.Select(source =>
            {
                var stats = leadCountsGrouped.FirstOrDefault(x => x.SourceId == source.Id);
                return new LeadCountBySourceViewModel
                {
                    SourceName = source.Name,
                    TotalLeads = stats?.Count ?? 0
                };
            })
            .OrderBy(x => x.SourceName)
            .ToList();

            return result;
        }


        public async Task<List<LeadCountByBranchModel>> GetLeadCountByBranchAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            // 1. Get all branches
            var allBranches = await _context.branches.ToListAsync();

            // 2. Get Lead Counts grouped by Branch for current year
            var leadCountsGrouped = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .GroupBy(l => l.BranchId)
                .Select(g => new
                {
                    BranchId = g.Key,
                    TotalCount = g.Count(),
                    ConvertedCount = g.Count(l => l.Status.Name == "Converted") // Note: Use Status name check if IsConverted is not reliable in DB or for consistency
                })
                .ToListAsync();

            // 3. Left Join in memory
            var result = allBranches.Select(branch =>
            {
                var stats = leadCountsGrouped.FirstOrDefault(x => x.BranchId == branch.Id);
                var totalCount = stats?.TotalCount ?? 0;
                var convertedCount = stats?.ConvertedCount ?? 0;

                return new LeadCountByBranchModel
                {
                    BranchName = branch.BranchName,
                    ConvertedCount = convertedCount,
                    TotalCount = totalCount,
                    SuccessPercentage = totalCount > 0 
                        ? Math.Round((convertedCount * 100.0) / totalCount, 2) 
                        : 0
                };
            })
            .OrderBy(x => x.BranchName)
            .ToList();

            return result;
        }



    }
}

