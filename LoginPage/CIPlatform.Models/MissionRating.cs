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
        public User User { get; set; }

        [JsonIgnore]
        public Mission Mission { get; set; } // Navigation property for Mission entity
    }
}
