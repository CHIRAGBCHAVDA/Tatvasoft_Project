using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CIPlatform.Models
{
    public partial class Availability
    {
        public Availability()
        {
            Users = new HashSet<User>();
        }

        public byte AvailabilityId { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
