using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CIPlatform.Models
{
    public partial class MissionRating
    {
        public long MissionRatingId { get; set; }
        public long UserId { get; set; }
        public long MissionId { get; set; }
        public long? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public virtual Mission Mission { get; set; } = null!;
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}
