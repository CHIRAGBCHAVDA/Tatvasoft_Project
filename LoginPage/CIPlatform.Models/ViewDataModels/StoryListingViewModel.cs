using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class StoryListingViewModel
    {
        public Story Stories { get; set;}
        public User Users { get; set;}
        public Mission Missions { get; set;}
        public string Theme { get; set;}
        public List<string>? Skills { get; set;}
        public string? City { get; set; }
        public string? ImgLink{ get; set; }
    }
}
