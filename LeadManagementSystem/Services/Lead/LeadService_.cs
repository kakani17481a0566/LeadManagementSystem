using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadManagementSystem.Data;
using LeadManagementSystem.ViewModel.Lead;

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

    }
}

