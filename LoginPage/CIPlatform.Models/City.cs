using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CIPlatform.Models
{
    public partial class City
    {
        public City()
        {
            Missions = new HashSet<Mission>();
            Users = new HashSet<User>();
        }

        public long CityId { get; set; }
        public long? CountryId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public virtual Country? Country { get; set; }
        [JsonIgnore]
        public virtual ICollection<Mission> Missions { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
