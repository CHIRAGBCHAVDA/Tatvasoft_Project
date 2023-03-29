using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CIPlatform.Models
{
    public partial class FavouriteMission
    {
        public long FavouriteMissionId { get; set; }
        public long UserId { get; set; }
        public long MissionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [JsonIgnore]
        public User User { get; set; } // Navigation property for User entity
        [JsonIgnore]
        public Mission Mission { get; set; } // Navigation property for Mission entity
    }
}
