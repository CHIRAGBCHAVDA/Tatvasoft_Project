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
        
        public List<SelectListItem>? City { get; set; }
        public List<SelectListItem>? Country { get; set; }
        public List<SelectListItem>? MissionTheme { get; set; }
        public List<SelectListItem>? Skills { get; set; }    

    }
}
