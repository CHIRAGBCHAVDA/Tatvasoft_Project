using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminSkillsViewModel
    {
        public long SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
        public byte Status { get; set; }
    }
}
