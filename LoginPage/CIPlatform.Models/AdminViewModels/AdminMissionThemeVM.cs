using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminMissionThemeVM
    {
        public long MissionThemeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public byte Status { get; set; }
    }
}
