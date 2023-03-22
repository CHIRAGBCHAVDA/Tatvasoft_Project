using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class ShareStoryViewModel
    {
        public long MissionId { get; set; }
        public string StoryTitle { get; set; }
        public DateTime Date { get; set; }
        public string MyStory { get; set; }
        public string? VideoUrl { get; set; }
        public List<String>? Photos { get; set; }
    }
}
