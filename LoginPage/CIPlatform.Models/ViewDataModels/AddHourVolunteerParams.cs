using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class AddHourVolunteerParams
    {
        public long MissionId { get; set; }
        public DateTime VolunteeredDate { get; set; }
        public string Message { get; set; } = string.Empty;
        public TimeSpan FormattedTime { get; set; }
        
    }
}
