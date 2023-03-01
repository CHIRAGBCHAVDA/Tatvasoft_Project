using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class StoryInvite
    {
        public long StoryInviteId { get; set; }
        public long StoryId { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
        public DateTime? CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
