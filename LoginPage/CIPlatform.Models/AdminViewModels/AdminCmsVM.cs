using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminCmsVM
    {
        public long? CMSId { get; set; } = 0;
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public bool? Status { get; set; } = null;
    }
}
