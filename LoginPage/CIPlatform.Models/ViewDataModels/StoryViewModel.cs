using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class StoryViewModel
    {
        public List<StoryListingViewModel> StoryListing { get; set; } = new List<StoryListingViewModel> { };
        public MissionListingHeader StoryListingHeader { get; set; }
    }
}
