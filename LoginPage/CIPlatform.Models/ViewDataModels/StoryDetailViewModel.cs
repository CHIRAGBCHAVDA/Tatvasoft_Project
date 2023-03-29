using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class StoryDetailViewModel
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string? WhyIVolunteer { get; set; }
        public List<User> Users { get; set; }   
        public ShareStoryViewModel ShareStory { get; set; }
        public long? totalViews { get; set; }
    }
}
