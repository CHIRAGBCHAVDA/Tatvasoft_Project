using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class MissionTheme
    {
        public long MissionThemeId { get; set; }
        public string Title { get; set; } = null!;
        public byte Status { get; set; }
        public DateTime? CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
