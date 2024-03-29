﻿using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            ContactUs = new HashSet<ContactU>();
            FavouriteMissions = new HashSet<FavouriteMission>();
            MissionApplications = new HashSet<MissionApplication>();
            MissionRatings = new HashSet<MissionRating>();
            Stories = new HashSet<Story>();
            StoryInviteFromUsers = new HashSet<StoryInvite>();
            StoryInviteToUsers = new HashSet<StoryInvite>();
            Timesheets = new HashSet<Timesheet>();
            UserNotificationPrefs = new HashSet<UserNotificationPref>();
            UserNotifications = new HashSet<UserNotification>();
            UserSkills = new HashSet<UserSkill>();
        }

        public long UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Token { get; set; }
        public string? PhoneNumber { get; set; }
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
        public string? ManagerDetails { get; set; }
        public byte? AvailabilityId { get; set; }
        public byte Role { get; set; }

        public virtual Availability? Availability { get; set; }
        public virtual City City { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ContactU> ContactUs { get; set; }
        public virtual ICollection<FavouriteMission> FavouriteMissions { get; set; }
        public virtual ICollection<MissionApplication> MissionApplications { get; set; }
        public virtual ICollection<MissionRating> MissionRatings { get; set; }
        public virtual ICollection<Story> Stories { get; set; }
        public virtual ICollection<StoryInvite> StoryInviteFromUsers { get; set; }
        public virtual ICollection<StoryInvite> StoryInviteToUsers { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
        public virtual ICollection<UserNotificationPref> UserNotificationPrefs { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
