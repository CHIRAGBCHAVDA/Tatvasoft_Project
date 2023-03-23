using System;
using System.Collections.Generic;

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

        public User User { get; set; } // Navigation property for User entity
        public Mission Mission { get; set; } // Navigation property for Mission entity
    }
}
