using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminBannerViewModel
    {
        public long BannerId { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Heading { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
    }
}
