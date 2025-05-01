using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    [Table("call_log")] // Maps this class to the "call_log" table in the database
    public class CallLog
    {
        [Key]
        [Column("log_date")] // Maps to "log_date" column
        public DateTime LogDate { get; set; }

        [Column("incoming_calls")] // Maps to "incoming_calls" column
        public int IncomingCalls { get; set; }

        [Column("outgoing_calls")] // Maps to "outgoing_calls" column
        public int OutgoingCalls { get; set; }
    }
}
