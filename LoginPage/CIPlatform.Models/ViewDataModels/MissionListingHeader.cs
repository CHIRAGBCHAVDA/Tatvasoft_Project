using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPlatform.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CIPlatform.Models.ViewDataModels
{
    public class MissionListingHeader
    {
        
        public List<Country>? Country { get; set; }
        public List<City>? City { get; set; }
        public List<MissionTheme>? MissionTheme { get; set; }
        public List<Skill>? Skills { get; set; }

        public List<Mission>? Mission { get; set; }

    }
}
