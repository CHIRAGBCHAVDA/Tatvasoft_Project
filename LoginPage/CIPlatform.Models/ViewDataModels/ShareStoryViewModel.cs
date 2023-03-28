using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class ShareStoryViewModel
    {
        //public List<string> MissionNames{ get; set; }
        public string StoryTitle { get; set; }
        public DateTime Date { get; set; }
        public string MyStory { get; set; }
        public List<string>? VideoUrl { get; set; }
        public List<string>? Photos { get; set; }
        public long MissionId { get; set; }
    }
}
