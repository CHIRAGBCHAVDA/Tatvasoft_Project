using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminStoryVM
    {
        public long StoryId { get; set; }
        public string StoryTitle { get; set; }
        public string FullName { get; set; }
        public string MissionTitle { get; set; }
        public long MissionId { get; set; }
    }
}
