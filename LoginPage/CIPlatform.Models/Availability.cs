﻿using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class Availability
    {
        public Availability()
        {
            Missions = new HashSet<Mission>();
            Users = new HashSet<User>();
        }

        public byte AvailabilityId { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
