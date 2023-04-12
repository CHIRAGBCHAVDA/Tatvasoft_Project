using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class TimeBasedTimesheetViewModel
    {
        public long TimesheetId { get; set; }
        public long MissionId { get; set; }
        public string MissionName { get; set; } = string.Empty;
        public TimeSpan? Time { get; set; }
        public DateTime DateVolunteered { get; set; }
        public string? Notes { get; set; }
        public byte ApprovalStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
