using LeadManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeadManagementSystem.Services.Calllog
{
    public interface ICallLogService
    {
        // CRUD Methods
        Task<IEnumerable<CallLog>> GetAllAsync();                // Get all call logs
        Task<CallLog?> GetByDateAsync(DateTime logDate);         // Get a call log by its date
        Task AddAsync(CallLog callLog);                           // Create a new call log
        Task UpdateAsync(CallLog callLog);                        // Update an existing call log
        Task DeleteAsync(DateTime logDate);                       // Delete a call log by date

        // Analysis Methods
        Task<IEnumerable<dynamic>> GetTotalCallsByDateAsync();   // Get total calls by date
        Task<IEnumerable<dynamic>> GetMonthlyStatsAsync();       // Get monthly aggregated stats
        Task<IEnumerable<dynamic>> GetCallRatioAnalysisAsync();  // Get call ratio analysis

        Task<IEnumerable<dynamic>> GetMonthlyCallRatioAnalysisAsync(); // Get monthly aggregated call ratio analysis

    }
}
