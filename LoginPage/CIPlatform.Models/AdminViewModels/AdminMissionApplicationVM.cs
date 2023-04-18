using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminMissionApplicationVM
    {
        public long MissionApplicationId { get; set; }
        public string MissionTitle { get; set; }
        public long MissionId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
