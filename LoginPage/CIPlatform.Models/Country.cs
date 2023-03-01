using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public long CountryId { get; set; }
        public string Name { get; set; } = null!;
        public string Iso { get; set; } = null!;
        public DateTime? CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
