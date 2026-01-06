using LeadManagementSystem.Models;
using LeadManagementSystem.Services.Lead;
using LeadManagementSystem.ViewModel.Lead;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeadManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadSummaryController : ControllerBase
    {
        private readonly LeadService_ _leadService;
        private readonly ILogger<LeadSummaryController> _logger;

        public LeadSummaryController(LeadService_ leadService, ILogger<LeadSummaryController> logger)
        {
            _leadService = leadService;
            _logger = logger;
        }

        // GET: api/LeadSummary/LeadCountByStatus
        //[HttpGet("LeadCountByStatusCurrentYearMonth")]
        //public async Task<ActionResult<object>> GetLeadCountByStatus()
        //{
        //    _logger.LogInformation("Getting lead count by status and total lead count for the current year...");

        //    var (leadCountByStatus, totalLeadCount) = await _leadService.GetLeadCountByStatusAndTotalCountAsync();

        //    if (leadCountByStatus == null || leadCountByStatus.Count == 0)
        //    {
        //        return NotFound("No data found for lead count by status.");
        //    }

        //    return Ok(new
        //    {
        //        TotalLeadCount = totalLeadCount,
        //        LeadCountByStatus = leadCountByStatus
        //    });
        //}

        // GET: api/LeadSummary/DailyStatusSummary
        //[HttpGet("DailyStatusSummaryCurrentMonthDay")]
        //public async Task<ActionResult<object>> GetDailyLeadStatusSummary()
        //{
        //    _logger.LogInformation("Getting daily lead status summary and total lead count for the current month...");

        //    var (dailySummary, totalLeadCount) = await _leadService.GetDailyLeadStatusSummaryAsync();

        //    if (dailySummary == null || dailySummary.Count == 0)
        //    {
        //        return NotFound("No daily summary data found.");
        //    }

        //    return Ok(new
        //    {
        //        TotalLeadCount = totalLeadCount,
        //        DailySummary = dailySummary
        //    });
        //}

        // GET: api/LeadSummary/LeadCountBySourceAndStatus
        //[HttpGet("LeadCountBySourceAndStatusCurrentYear")]
        //public async Task<ActionResult<List<LeadCountBySourceAndStatusViewModel>>> GetLeadCountBySourceAndStatus()
        //{
        //    _logger.LogInformation("Getting lead count by source and status for the current year...");

        //    var result = await _leadService.GetLeadCountBySourceAndStatusAsync();

        //    if (result == null || result.Count == 0)
        //    {
        //        return NotFound("No data found for lead count by source and status.");
        //    }

        //    return Ok(result);
        //}

        // GET: api/LeadSummary/LeadCountBySourceAndStatusByPeriod
        //[HttpGet("LeadCountBySourceAndStatusByPeriod")]
        //public async Task<ActionResult<List<LeadCountBySourceAndStatusByYearViewModel>>> GetLeadCountBySourceAndStatusByPeriod(
        //    [FromQuery] int startYear,
        //    [FromQuery] int startMonth,
        //    [FromQuery] int endYear,
        //    [FromQuery] int endMonth)
        //{
        //    _logger.LogInformation($"Getting lead count by source and status from {startYear}-{startMonth} to {endYear}-{endMonth}...");

        //    var result = await _leadService.GetLeadCountBySourceAndStatusByPeriodAsync(startYear, startMonth, endYear, endMonth);

        //    if (result == null || result.Count == 0)
        //    {
        //        return NotFound("No data found for lead count by source and status in the specified period.");
        //    }

        //    return Ok(result);
        //}

        // GET: api/LeadSummary/GetLeadStatusSummary
        //[HttpGet("GetLeadStatusSummaryCurrentYearCurrentMonth")]
        //public async Task<ActionResult<object>> GetLeadStatusSummary([FromQuery] string periodType)
        //{
        //    _logger.LogInformation($"Getting lead status summary for period type: {periodType}...");

        //    if (string.IsNullOrEmpty(periodType))
        //    {
        //        return BadRequest("The periodType parameter is required.");
        //    }

        //    if (periodType.Equals("CurrentYear", StringComparison.OrdinalIgnoreCase))
        //    {
        //        var (leadCountByStatus, totalLeadCount) = await _leadService.GetLeadCountByStatusAndTotalCountAsync();

        //        if (leadCountByStatus == null || leadCountByStatus.Count == 0)
        //        {
        //            return NotFound("No data found for lead count by status for the current year.");
        //        }

        //        return Ok(new
        //        {
        //            TotalLeadCount = totalLeadCount,
        //            LeadCountByStatus = leadCountByStatus
        //        });
        //    }
        //    else if (periodType.Equals("CurrentMonth", StringComparison.OrdinalIgnoreCase))
        //    {
        //        var (dailySummary, totalLeadCount) = await _leadService.GetDailyLeadStatusSummaryAsync();

        //        if (dailySummary == null || dailySummary.Count == 0)
        //        {
        //            return NotFound("No daily summary data found for the current month.");
        //        }

        //        return Ok(new
        //        {
        //            TotalLeadCount = totalLeadCount,
        //            DailySummary = dailySummary
        //        });
        //    }
        //    else
        //    {
        //        return BadRequest("Invalid periodType. Please provide either 'CurrentYear' or 'CurrentMonth'.");
        //    }
        //}


        // GET: api/LeadSummary/LeadCountBySourceBranchMonth
        [HttpGet("LeadCountBySourceBranchMonth")]
        public async Task<ActionResult<List<LeadCountBySourceBranchMonthViewModel>>> GetLeadCountBySourceBranchMonth()
        {
            _logger.LogInformation("Getting lead count by source, branch, and month for the current year...");

            var result = await _leadService.GetLeadCountBySourceBranchMonthAsync();

            if (result == null || result.Count == 0)
            {
                return NotFound("No data found for lead count by source, branch, and month.");
            }

            return Ok(result);


        }


        // GET: api/LeadSummary/DailyLeadCountBySourceAndBranchCurrentMonth
        //[HttpGet("DailyLeadCountBySourceAndBranchCurrentMonth")]
        //public async Task<ActionResult<List<DailyLeadCountBySourceBranchViewModel>>> GetDailyLeadCountBySourceAndBranchCurrentMonth()
        //{
        //    _logger.LogInformation("Getting daily lead count by source, branch, and day for the current month...");

        //    var result = await _leadService.GetDailyLeadCountBySourceAndBranchCurrentMonthAsync();

        //    if (result == null || result.Count == 0)
        //    {
        //        return NotFound("No data found for daily lead count by source, branch, and day for the current month.");
        //    }

        //    return Ok(result);
        //}

        // using
        // GET: api/LeadSummary/LeadCountBySourceAndBranch
        [HttpGet("LeadCountBySourceAndBranch")]
        public async Task<ActionResult<List<LeadCountBySourceAndBranchViewModel>>> GetLeadCountBySourceAndBranch()
        {
            _logger.LogInformation("Getting lead count by source and branch...");

            var result = await _leadService.GetLeadCountBySourceAndBranchAsync();

            if (result == null || result.Count == 0)
            {
                return NotFound("No data found for lead count by source and branch.");
            }

            return Ok(result);
        }


     //   [HttpGet("LeadCountByDateRange")]
     //   public async Task<IActionResult> GetLeadCountByDateRange(
     //[FromQuery] int startYear,
     //[FromQuery] int startMonth,
     //[FromQuery] int startDay,
     //[FromQuery] int endYear,
     //[FromQuery] int endMonth,
     //[FromQuery] int endDay)
     //   {
     //       _logger.LogInformation(
     //           "Fetching lead count from {StartDate} to {EndDate}",
     //           new DateTime(startYear, startMonth, startDay).ToShortDateString(),
     //           new DateTime(endYear, endMonth, endDay).ToShortDateString());

     //       try
     //       {
     //           var result = await _leadService.DateTimeGetLeadCountByDateRangeAsync(
     //               startYear, startMonth, startDay, endYear, endMonth, endDay);

     //           if (result == null || result.Count == 0)
     //           {
     //               return NotFound("No leads found for the specified date range.");
     //           }

     //           // Return the results grouped by Year, Month, and Day
     //           return Ok(result);
     //       }
     //       catch (Exception ex)
     //       {
     //           _logger.LogError(ex, "Error while fetching lead count.");
     //           return StatusCode(500, "Internal server error.");
     //       }
     //   }


        [HttpGet("lead-count-by-status-and-month")]
        public async Task<IActionResult> GetLeadCountByStatusAndMonth()
        {
            // Call the service to get lead count by status and month
            List<LeadCount> leadCountByStatus = await _leadService.GetLeadCountByStatusAndMonthAsync();

            // Return the result as JSON
            return Ok(leadCountByStatus);
        }

        [HttpGet("lead_count_by_day_current_month_current_year")]
        public async Task<IActionResult> GetLeadCountByDay()
        {
            List<LeadCountByDayViewModel> leadCountByDay = await _leadService.GetLeadCountByDayAsync();
            return Ok(leadCountByDay);
        }

        [HttpGet("lead-count-by-source-current-year")]
        public async Task<IActionResult> GetLeadCountBySource()
        {
            var result = await _leadService.GetLeadCountBySourceAsync();
            return Ok(result);
        }

        // GET: api/LeadSummary/LeadCountByBranch
        [HttpGet("LeadCountByBranch-SuccessPercentage")]
        public async Task<ActionResult<List<LeadCountByBranchModel>>> GetLeadCountByBranch()
        {
            _logger.LogInformation("Fetching lead count by branch for the current year...");

            var leadCountByBranch = await _leadService.GetLeadCountByBranchAsync();

            if (leadCountByBranch == null || leadCountByBranch.Count == 0)
            {
                // Return empty list instead of 404 for dashboard compatibility
                return Ok(new List<LeadCountByBranchModel>());
            }

            return Ok(leadCountByBranch);
        }





        // GET: api/LeadSummary/LeadStats
        [HttpGet("LeadStats")]
        public async Task<ActionResult<LeadStatsViewModel>> GetLeadStats()
        {
            try
            {
                // 1. Get Yearly Data (LeadCount list has months)
                var yearlyData = await _leadService.GetLeadCountByStatusAndMonthAsync();
                
                // 2. Get Monthly Data (Daily breakdown)
                var monthlyData = await _leadService.GetLeadCountByDayAsync();

                // 3. Construct LeadTotals (Aggregate from Yearly Data which covers current year)
                var totals = new LeadTotalsViewModel();
                if (yearlyData != null)
                {
                    foreach (var item in yearlyData)
                    {
                        totals.TotalLeads += item.TotalCount;
                        totals.ConvertedLeads += item.ConvertedCount;
                        totals.InProcessLeads += item.InProgress; // Assuming New is separate or included? Frontend says "Processing"
                        totals.NonConverted += item.NonConverted;
                    }
                    // Note: Check if 'New' counts should be added to InProcess or shown separately. 
                    // Frontend has "Processing" card. yearlyData has InProgress and New.
                    // Let's add 'New' to 'InProcessLeads' to match frontend "Inprocess" series logic if needed, 
                    // but looking at Overview.jsx series: "Inprogress" is one series.
                    // LeadCount.InProgress is likely what matches.
                }

                // 4. Construct Yearly Chart Data
                var yearlyChart = new ChartDataViewModel
                {
                    Categories = new List<object>(),
                    Series = new List<SeriesData>
                    {
                        new SeriesData { Name = "Leads", Data = new List<int>() },
                        new SeriesData { Name = "Inprogress", Data = new List<int>() },
                        new SeriesData { Name = "Converted", Data = new List<int>() },
                        new SeriesData { Name = "Closed", Data = new List<int>() }
                    }
                };

                if (yearlyData != null && yearlyData.Count > 0)
                {
                    foreach (var item in yearlyData)
                    {
                        yearlyChart.Categories.Add(item.Label); // "JAN", "FEB" etc.
                        yearlyChart.Series[0].Data.Add(item.TotalCount);
                        yearlyChart.Series[1].Data.Add(item.InProgress);
                        yearlyChart.Series[2].Data.Add(item.ConvertedCount);
                        yearlyChart.Series[3].Data.Add(item.NonConverted);
                    }
                }

                // 5. Construct Monthly Chart Data
                var monthlyChart = new ChartDataViewModel
                {
                    Categories = new List<object>(),
                    Series = new List<SeriesData>
                    {
                        new SeriesData { Name = "Leads", Data = new List<int>() },
                        new SeriesData { Name = "Inprogress ", Data = new List<int>() }, // Note space in name matches frontend
                        new SeriesData { Name = "Converted", Data = new List<int>() },
                        new SeriesData { Name = "Closed", Data = new List<int>() }
                    }
                };

                if (monthlyData != null && monthlyData.Count > 0)
                {
                    foreach (var item in monthlyData)
                    {
                        monthlyChart.Categories.Add(item.Day);
                        monthlyChart.Series[0].Data.Add(item.TotalCount);
                        monthlyChart.Series[1].Data.Add(item.InProgress); // Add NewCount here if needed?
                        monthlyChart.Series[2].Data.Add(item.ConvertedCount);
                        monthlyChart.Series[3].Data.Add(item.NonConverted);
                    }
                }

                return Ok(new LeadStatsViewModel
                {
                    LeadTotals = totals,
                    Yearly = yearlyChart,
                    Monthly = monthlyChart
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching LeadStats");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
