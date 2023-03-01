using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class StoryStatus
    {
        public byte StoryStatusId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
