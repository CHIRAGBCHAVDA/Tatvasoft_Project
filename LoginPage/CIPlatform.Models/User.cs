using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
        }

        public long UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Token { get; set; }
        public long PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public string? WhyIVolunteer { get; set; }
        public string? EmployeeId { get; set; }
        public string? Department { get; set; }
        public long CityId { get; set; }
        public long CountryId { get; set; }
        public string? ProfileText { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? Title { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
