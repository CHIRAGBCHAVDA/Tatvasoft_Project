using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class MissionListingCard 
    {
        public Mission mission { get; set; }
        public string? MissionTheme { get; set; }
        public string? City { get; set; }
        public MissionSkill missionSkill { get; set; }
        public List<string>? Skills { get; set; }
        public string? ImageLink { get; set; }
        public List<MissionRating> rating { get; set; }
    }
}
