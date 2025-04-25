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

        public async Task<(List<LeadCountByStatusViewModel> LeadCountByStatus, int TotalLeadCount)> GetLeadCountByStatusAndTotalCountAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            // Get lead count by status, month, and year
            var leadCountByStatus = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)  // Ensure 'DateTime' exists in your entity
                .GroupBy(l => new
                {
                    l.Status.Name,  // Ensure 'Status' is a navigation property
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

            // Get the total count of leads for the current year
            var totalLeadCount = await _context.leads
                .Where(l => l.DateTime.Year == currentYear)
                .CountAsync();

            // Return both the lead count by status and the total lead count
            return (leadCountByStatus, totalLeadCount);
        }


        public async Task<(List<DailyLeadStatusSummaryViewModel> DailySummary, int TotalLeadCount)> GetDailyLeadStatusSummaryAsync()
        {
            var now = DateTime.UtcNow;
            var currentYear = now.Year;
            var currentMonth = now.Month;

            // Daily grouped summary by status
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

            // Total leads for the current month
            var totalLeadCount = await _context.leads
                .Where(l => l.DateTime.Year == currentYear && l.DateTime.Month == currentMonth)
                .CountAsync();

            return (summary, totalLeadCount);
        }



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

        public async Task<List<LeadCountBySourceAndStatusByYearViewModel>> GetLeadCountBySourceAndStatusByYearAsync(int startYear, int endYear)
        {
            var leadCountBySourceAndStatus = await _context.leads
                .Where(l => l.DateTime.Year >= startYear && l.DateTime.Year <= endYear)
                .GroupBy(l => new
                {
                    l.Status.Name,
                    SourceName = l.LeadSource.Name,
                    Year = l.DateTime.Year
                })
                .Select(g => new LeadCountBySourceAndStatusByYearViewModel
                {
                    StatusName = g.Key.Name,
                    SourceName = g.Key.SourceName,
                    LeadCount = g.Count(),
                    Year = g.Key.Year
                })
                .OrderBy(r => r.SourceName)
                .ThenBy(r => r.Year)
                .ToListAsync();

            return leadCountBySourceAndStatus;
        }





    }

}

