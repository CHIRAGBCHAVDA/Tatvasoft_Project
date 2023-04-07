using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace CIPlatform.Models;

public partial class TblUser
{
    public long UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    [NotMapped]
    [Compare("Password", ErrorMessage = "Confirm password must be matched with Password!!")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "You must provide a phone number")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    public decimal PhoneNumber { get; set; }

    public string? Avatar { get; set; }

    public string? WhyIVolunteer { get; set; }

    public string? EmployeeId { get; set; }

    public string? Department { get; set; }

    public long? CityId { get; set; }

    public long? CountryId { get; set; }

    public string? ProfileText { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? Title { get; set; }

    public int? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
    [JsonIgnore]

    public virtual TblCity? City { get; set; }
    [JsonIgnore]

    public virtual TblCountry? Country { get; set; }
    [JsonIgnore]

    public virtual Status? StatusNavigation { get; set; }

    public string? Passcode { get; set; }
}
