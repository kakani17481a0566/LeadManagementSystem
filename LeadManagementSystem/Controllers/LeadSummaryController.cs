using LeadManagementSystem.Models;
using LeadManagementSystem.Services.Lead;
using LeadManagementSystem.ViewModel.Lead;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

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
                return NotFound("No data found for lead count by branch.");
            }

            return Ok(leadCountByBranch);
        }


        [HttpGet("LeadStats")]
        public async Task<ActionResult<LeadStats>> GetLeadStats()
        {
            LeadStats lesdStats = new LeadStats();
            lesdStats.yearly = new Period();
            lesdStats.monthly = new Period();
            lesdStats.leadTotals = new LeadTotals();
            lesdStats.MonthlyTotalLeads = new LeadTotals();



            Series TotalCountseries = new Series();
            TotalCountseries.Name = "TotalCount";
            TotalCountseries.Data = new int[12];


            Series Closedseries = new Series();
            Closedseries.Name = "Closed";
            Closedseries.Data = new int[12];


            Series InProcessseries = new Series();
            InProcessseries.Name = "InProcess";
            InProcessseries.Data = new int[12];


            Series Openseries = new Series();
            Openseries.Name = "Open";
            Openseries.Data = new int[12];

            Series Not_Interestedseries = new Series();
            Not_Interestedseries.Name = "Not_Interested";
            Not_Interestedseries.Data = new int[12];


            List<LeadCount> leadCountByStatus = await _leadService.GetLeadCountByStatusAndMonthAsync();

            if (leadCountByStatus == null || !leadCountByStatus.Any())
            {
                return NotFound("No lead stats found for the current year.");
            }

            List<string> categories = new List<string>();
            int i = -1;
            foreach (var item in leadCountByStatus)
            {
                TotalCountseries.Data[++i] = item.TotalCount;
                
                Closedseries.Data[i] = item.Closed;
                InProcessseries.Data[i] = item.InProcess;
                Openseries.Data[i] = item.Open;
                Not_Interestedseries.Data[i] = item.NotInterested;

                categories.Add(item.Label);

                lesdStats.leadTotals.TotalLeads += item.TotalCount;
                lesdStats.leadTotals.ConvertedLeads += item.Closed;
                lesdStats.leadTotals.NonConverted += item.NotInterested;
                lesdStats.leadTotals.InProcessLeads += item.InProcess;



            }

            lesdStats.yearly.series.Add(TotalCountseries);
            lesdStats.yearly.series.Add(Closedseries);
            lesdStats.yearly.series.Add(InProcessseries);
            lesdStats.yearly.series.Add(Openseries);
            lesdStats.yearly.series.Add(Not_Interestedseries);
            lesdStats.yearly.Categories = categories.ToArray();


            //month


            TotalCountseries = new Series();
            TotalCountseries.Name = "TotalCount";
            TotalCountseries.Data = new int[31];


              Closedseries = new Series();
            Closedseries.Name = "Closed";
            Closedseries.Data = new int[31];


              InProcessseries = new Series();
            InProcessseries.Name = "InProcess";
            InProcessseries.Data = new int[31];


              Openseries = new Series();
            Openseries.Name = "Open";
            Openseries.Data = new int[31];

              Not_Interestedseries = new Series();
            Not_Interestedseries.Name = "Not_Interested";
            Not_Interestedseries.Data = new int[31];

            List<LeadCountByDayViewModel> leadCountByDay = await _leadService.GetLeadCountByDayAsync();


             categories = new List<string>();
              i = -1;
            foreach (var item in leadCountByDay)
            {
                TotalCountseries.Data[++i] = item.TotalCount;
                Closedseries.Data[i] = item.Closed;
                InProcessseries.Data[i] = item.InProcess;
                Openseries.Data[i] = item.Open;
                Not_Interestedseries.Data[i] = item.NotInterested;

                categories.Add(item.Day.ToString());

                lesdStats.MonthlyTotalLeads.TotalLeads += item.TotalCount;
                lesdStats.MonthlyTotalLeads.ConvertedLeads += item.Closed;
                lesdStats.MonthlyTotalLeads.NonConverted += item.NotInterested;
                lesdStats.MonthlyTotalLeads.InProcessLeads += item.InProcess;

            }

            lesdStats.monthly.series.Add(TotalCountseries);
            lesdStats.monthly.series.Add(Closedseries);
            lesdStats.monthly.series.Add(InProcessseries);
            lesdStats.monthly.series.Add(Openseries);
            lesdStats.monthly.series.Add(Not_Interestedseries);
            lesdStats.monthly.Categories = categories.ToArray();
            






            return Ok(lesdStats);
        }




    }

}
