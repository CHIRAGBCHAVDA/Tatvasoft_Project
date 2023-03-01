using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class MissionRating
    {
        public long MissionRatingId { get; set; }
        public long UserId { get; set; }
        public long MissionId { get; set; }
        public byte? Rating { get; set; }
        public DateTime? CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
