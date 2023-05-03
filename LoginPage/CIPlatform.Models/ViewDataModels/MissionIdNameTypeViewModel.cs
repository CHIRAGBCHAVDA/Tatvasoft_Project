using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class MissionIdNameTypeViewModel
    {
        public long MissionId { get; set; }
        public string MissionName { get; set; }
        public bool IsGoalBased { get; set; }
        public DateTime? enddate { get; set; }
    }
}
