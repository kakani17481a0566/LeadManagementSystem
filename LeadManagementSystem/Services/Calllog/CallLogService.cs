using LeadManagementSystem.Data;
using LeadManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadManagementSystem.Services.Calllog
{
    public class CallLogService : ICallLogService
    {
        private readonly ApplicationDbContext _context;

        public CallLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CallLog>> GetAllAsync()
        {
            return await _context.CallLogs.ToListAsync();
        }

        public async Task<CallLog?> GetByDateAsync(DateTime logDate)
        {
            return await _context.CallLogs
                .FirstOrDefaultAsync(cl => cl.LogDate.Date == logDate.Date);
        }

        public async Task AddAsync(CallLog callLog)
        {
            _context.CallLogs.Add(callLog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CallLog callLog)
        {
            var existingLog = await _context.CallLogs
                .FirstOrDefaultAsync(cl => cl.LogDate.Date == callLog.LogDate.Date);

            if (existingLog != null)
            {
                existingLog.IncomingCalls = callLog.IncomingCalls;
                existingLog.OutgoingCalls = callLog.OutgoingCalls;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(DateTime logDate)
        {
            var callLog = await _context.CallLogs
                .FirstOrDefaultAsync(cl => cl.LogDate.Date == logDate.Date);

            if (callLog != null)
            {
                _context.CallLogs.Remove(callLog);
                await _context.SaveChangesAsync();
            }
        }

        // Analysis Methods
        public async Task<IEnumerable<dynamic>> GetTotalCallsByDateAsync()
        {
            return await _context.CallLogs
                .GroupBy(cl => cl.LogDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalIncomingCalls = g.Sum(cl => cl.IncomingCalls),
                    TotalOutgoingCalls = g.Sum(cl => cl.OutgoingCalls)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> GetMonthlyStatsAsync()
        {
            return await _context.CallLogs
                .GroupBy(cl => new { cl.LogDate.Year, cl.LogDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalIncomingCalls = g.Sum(cl => cl.IncomingCalls),
                    TotalOutgoingCalls = g.Sum(cl => cl.OutgoingCalls)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> GetCallRatioAnalysisAsync()
        {
            return await _context.CallLogs
                .GroupBy(cl => cl.LogDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    IncomingCalls = g.Sum(cl => cl.IncomingCalls),
                    OutgoingCalls = g.Sum(cl => cl.OutgoingCalls),
                    CallRatio = (double)(g.Sum(cl => cl.IncomingCalls)) / g.Sum(cl => cl.OutgoingCalls)
                })
                .ToListAsync();
        }
    }
}
