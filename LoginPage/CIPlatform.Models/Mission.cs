using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class Mission
    {
        public Mission()
        {
            Comments = new HashSet<Comment>();
        }

        public long MissionId { get; set; }
        public long MissionThemeId { get; set; }
        public long CityId { get; set; }
        public long CountryId { get; set; }
        public string Title { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte MissionTypeId { get; set; }
        public bool Status { get; set; }
        public string? OrganizationName { get; set; }
        public string? OrganizationDetail { get; set; }
        public byte? AvailabilityId { get; set; }
        public DateTime? CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
