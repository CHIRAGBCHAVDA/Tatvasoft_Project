using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CIPlatform.Models;

public partial class TblCity
{
    public long CityId { get; set; }

    public long? CountryId { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
    [JsonIgnore]
    public virtual TblCountry? Country { get; set; }
    [JsonIgnore]
    public virtual ICollection<TblUser> TblUsers { get; } = new List<TblUser>();
}
