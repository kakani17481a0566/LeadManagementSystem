using LeadManagementSystem.Models;
using LeadManagementSystem.Services.Calllog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace LeadManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallLogController : ControllerBase
    {
        private readonly ICallLogService _callLogService;

        public CallLogController(ICallLogService callLogService)
        {
            _callLogService = callLogService;
        }

        // GET: api/CallLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CallLog>>> GetAll()
        {
            var logs = await _callLogService.GetAllAsync();

            // Create a list to hold formatted logs with the LogDate in readable format
            var formattedLogs = new List<object>();
            foreach (var log in logs)
            {
                formattedLogs.Add(new
                {
                    LogDate = log.LogDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), // Format the date
                    log.IncomingCalls,
                    log.OutgoingCalls
                });
            }

            return Ok(formattedLogs);
        }

        // GET: api/CallLog/2025-05-01
        [HttpGet("{logDate:datetime}")]
        public async Task<ActionResult<CallLog>> GetByDate(DateTime logDate)
        {
            var log = await _callLogService.GetByDateAsync(logDate);
            if (log == null)
                return NotFound();

            // Return the formatted LogDate along with other properties
            var formattedLog = new
            {
                LogDate = log.LogDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), // Format the date
                log.IncomingCalls,
                log.OutgoingCalls
            };

            return Ok(formattedLog);
        }

        // POST: api/CallLog
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CallLog callLog)
        {
            if (callLog == null)
                return BadRequest("Call log data is required.");

            // Format and set LogDate as short date when creating the log (in case user sent in string format)
            if (callLog.LogDate != default)
            {
                callLog.LogDate = DateTime.ParseExact(callLog.LogDate.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            }

            await _callLogService.AddAsync(callLog);
            return CreatedAtAction(nameof(GetByDate), new { logDate = callLog.LogDate }, callLog);
        }

        // PUT: api/CallLog/2025-05-01
        [HttpPut("{logDate:datetime}")]
        public async Task<IActionResult> Update(DateTime logDate, [FromBody] CallLog callLog)
        {
            if (logDate != callLog.LogDate)
                return BadRequest("Mismatched log date.");

            await _callLogService.UpdateAsync(callLog);
            return NoContent();
        }

        // DELETE: api/CallLog/2025-05-01
        [HttpDelete("{logDate:datetime}")]
        public async Task<IActionResult> Delete(DateTime logDate)
        {
            await _callLogService.DeleteAsync(logDate);
            return NoContent();
        }

        // GET: api/CallLog/total
        [HttpGet("total")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetTotalCallsByDate()
        {
            var totalCalls = await _callLogService.GetTotalCallsByDateAsync();

            // Create a list to hold formatted total calls
            var formattedTotalCalls = new List<object>();
            foreach (var item in totalCalls)
            {
                formattedTotalCalls.Add(new
                {
                    Date = DateTime.Parse(item.Date.ToString()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), // Format the date
                    item.TotalIncomingCalls,
                    item.TotalOutgoingCalls
                });
            }

            return Ok(formattedTotalCalls);
        }

        // GET: api/CallLog/monthly-stats
        [HttpGet("monthly-stats")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetMonthlyStats()
        {
            var monthlyStats = await _callLogService.GetMonthlyStatsAsync();

            // Create a list to hold formatted monthly stats
            var formattedStats = new List<object>();
            foreach (var item in monthlyStats)
            {
                formattedStats.Add(new
                {
                    Date = new DateTime(item.Year, item.Month, 1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), // Format the date
                    item.TotalIncomingCalls,
                    item.TotalOutgoingCalls
                });
            }

            return Ok(formattedStats);
        }

        // GET: api/CallLog/call-ratio
        [HttpGet("call-ratio")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetCallRatioAnalysis()
        {
            var callRatioAnalysis = await _callLogService.GetCallRatioAnalysisAsync();

            // Create a list to hold formatted call ratio analysis
            var formattedAnalysis = new List<object>();
            foreach (var item in callRatioAnalysis)
            {
                formattedAnalysis.Add(new
                {
                    Date = DateTime.Parse(item.Date.ToString()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), // Format the date
                    item.IncomingCalls,
                    item.OutgoingCalls,
                    item.CallRatio
                });
            }

            return Ok(formattedAnalysis);
        }


        [HttpGet("call-ratio/monthly")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetMonthlyCallRatioAnalysis()
        {
            var monthlyCallRatioAnalysis = await _callLogService.GetMonthlyCallRatioAnalysisAsync();

            // Formatting the response if needed (like adding Month Year formatting)
            var formattedAnalysis = monthlyCallRatioAnalysis.Select(item => new
            {
                Month = new DateTime(item.Year, item.Month, 1).ToString("MMMM yyyy", CultureInfo.InvariantCulture), // Format as Month Year (e.g., May 2025)
                item.IncomingCalls,
                item.OutgoingCalls,
                item.CallRatio
            }).ToList();

            return Ok(formattedAnalysis);
        }

    }
}
