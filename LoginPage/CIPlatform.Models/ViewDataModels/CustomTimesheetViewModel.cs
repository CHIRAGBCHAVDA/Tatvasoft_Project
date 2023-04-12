using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class CustomTimesheetViewModel
    {
        public List<TimeBasedTimesheetViewModel> TimeBasedTimesheets { get; set; }
        public List<GoalBasedTimesheetViewModel> GoalBasedTimesheets { get; set; }
    }
}
