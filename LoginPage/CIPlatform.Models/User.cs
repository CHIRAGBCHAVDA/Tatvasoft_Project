using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIPlatform.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
        }

        public long UserId { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        //[Unique(ErrorMessage = "This email address is already in use.")]
        public string Email { get; set; } = null!;
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%&*?])[A-Za-z\d!@#$%&*?]{6}$",
            ErrorMessage ="The Password must be atleast of 6 characters long and contain atleast 1 digit and 1 special character.")]
        public string Password { get; set; } = null!;

        [NotMapped]
        [Compare("Password", ErrorMessage = "Confirm password must be matched with Password!!")]
        public string ConfirmPassword { get; set; }

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

        public DateTime? TokenCreatedAt { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FavouriteMission> FavouriteMissions { get; set; }


    }
}
