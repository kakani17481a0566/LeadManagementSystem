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
    public class LeadReportsController : ControllerBase
    {
        private readonly LeadService_ _leadService;
        private readonly ILogger<LeadReportsController> _logger;

        public LeadReportsController(LeadService_ leadService, ILogger<LeadReportsController> logger)
        {
            _leadService = leadService;
            _logger = logger;
        }

        // GET: api/LeadReports/LeadCountByStatus
        [HttpGet("LeadCountByStatus")]
        public async Task<ActionResult<object>> GetLeadCountByStatus()
        {
            _logger.LogInformation("Getting lead count by status and total lead count for the current year...");

            // Destructure the tuple into two variables
            var (leadCountByStatus, totalLeadCount) = await _leadService.GetLeadCountByStatusAndTotalCountAsync();

            if (leadCountByStatus == null || leadCountByStatus.Count == 0)
            {
                return NotFound("No data found for lead count by status.");
            }

            // Return both the lead count by status and the total lead count
            return Ok(new
            {
                TotalLeadCount = totalLeadCount,
                LeadCountByStatus = leadCountByStatus
            });
        }

        // GET: api/LeadReports/DailyStatusSummary
        [HttpGet("DailyStatusSummary")]
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


        // GET: api/LeadReports/MonthlyStatusSummary
        [HttpGet("MonthlyStatusSummary")]
        public async Task<ActionResult<List<MonthlyLeadStatusSummaryViewModel>>> GetMonthlyLeadStatusSummary()
        {
            _logger.LogInformation("Getting monthly lead status summary for the current year...");

            var result = await _leadService.GetMonthlyLeadStatusSummaryAsync();

            if (result == null || result.Count == 0)
            {
                return NotFound("No monthly summary data found.");
            }

            return Ok(result);
        }


    }
}
