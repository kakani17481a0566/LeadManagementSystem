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
        [HttpGet("LeadCountByStatusCurrentYearMonth")]
        public async Task<ActionResult<object>> GetLeadCountByStatus()
        {
            _logger.LogInformation("Getting lead count by status and total lead count for the current year...");

            var (leadCountByStatus, totalLeadCount) = await _leadService.GetLeadCountByStatusAndTotalCountAsync();

            if (leadCountByStatus == null || leadCountByStatus.Count == 0)
            {
                return NotFound("No data found for lead count by status.");
            }

            return Ok(new
            {
                TotalLeadCount = totalLeadCount,
                LeadCountByStatus = leadCountByStatus
            });
        }

        // GET: api/LeadSummary/DailyStatusSummary
        [HttpGet("DailyStatusSummaryCurrentMonthDay")]
        public async Task<ActionResult<object>> GetDailyLeadStatusSummary()
        {
            _logger.LogInformation("Getting daily lead status summary and total lead count for the current month...");

            var (dailySummary, totalLeadCount) = await _leadService.GetDailyLeadStatusSummaryAsync();

            if (dailySummary == null || dailySummary.Count == 0)
            {
                return NotFound("No daily summary data found.");
            }

            return Ok(new
            {
                TotalLeadCount = totalLeadCount,
                DailySummary = dailySummary
            });
        }

        // GET: api/LeadSummary/LeadCountBySourceAndStatus
        [HttpGet("LeadCountBySourceAndStatusCurrentYear")]
        public async Task<ActionResult<List<LeadCountBySourceAndStatusViewModel>>> GetLeadCountBySourceAndStatus()
        {
            _logger.LogInformation("Getting lead count by source and status for the current year...");

            var result = await _leadService.GetLeadCountBySourceAndStatusAsync();

            if (result == null || result.Count == 0)
            {
                return NotFound("No data found for lead count by source and status.");
            }

            return Ok(result);
        }

        // GET: api/LeadSummary/LeadCountBySourceAndStatusByPeriod
        [HttpGet("LeadCountBySourceAndStatusByPeriod")]
        public async Task<ActionResult<List<LeadCountBySourceAndStatusByYearViewModel>>> GetLeadCountBySourceAndStatusByPeriod(
            [FromQuery] int startYear,
            [FromQuery] int startMonth,
            [FromQuery] int endYear,
            [FromQuery] int endMonth)
        {
            _logger.LogInformation($"Getting lead count by source and status from {startYear}-{startMonth} to {endYear}-{endMonth}...");

            var result = await _leadService.GetLeadCountBySourceAndStatusByPeriodAsync(startYear, startMonth, endYear, endMonth);

            if (result == null || result.Count == 0)
            {
                return NotFound("No data found for lead count by source and status in the specified period.");
            }

            return Ok(result);
        }

        // GET: api/LeadSummary/GetLeadStatusSummary
        [HttpGet("GetLeadStatusSummaryCurrentYearCurrentMonth")]
        public async Task<ActionResult<object>> GetLeadStatusSummary([FromQuery] string periodType)
        {
            _logger.LogInformation($"Getting lead status summary for period type: {periodType}...");

            if (string.IsNullOrEmpty(periodType))
            {
                return BadRequest("The periodType parameter is required.");
            }

            if (periodType.Equals("CurrentYear", StringComparison.OrdinalIgnoreCase))
            {
                var (leadCountByStatus, totalLeadCount) = await _leadService.GetLeadCountByStatusAndTotalCountAsync();

                if (leadCountByStatus == null || leadCountByStatus.Count == 0)
                {
                    return NotFound("No data found for lead count by status for the current year.");
                }

                return Ok(new
                {
                    TotalLeadCount = totalLeadCount,
                    LeadCountByStatus = leadCountByStatus
                });
            }
            else if (periodType.Equals("CurrentMonth", StringComparison.OrdinalIgnoreCase))
            {
                var (dailySummary, totalLeadCount) = await _leadService.GetDailyLeadStatusSummaryAsync();

                if (dailySummary == null || dailySummary.Count == 0)
                {
                    return NotFound("No daily summary data found for the current month.");
                }

                return Ok(new
                {
                    TotalLeadCount = totalLeadCount,
                    DailySummary = dailySummary
                });
            }
            else
            {
                return BadRequest("Invalid periodType. Please provide either 'CurrentYear' or 'CurrentMonth'.");
            }
        }
    }
}
