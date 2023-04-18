using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminMissionVM
    {
        public long MissionId { get; set; }
        public string MissionTitle { get; set; }
        public Byte MissionTypeId{ get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
