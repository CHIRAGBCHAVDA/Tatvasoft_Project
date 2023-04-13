using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class EditGoalVolunteerParams
    {
        public long TimesheetId { get; set; }
        public long MissionId { get; set; }
        public DateTime VolunteeredDate { get; set; }
        public int Action { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
