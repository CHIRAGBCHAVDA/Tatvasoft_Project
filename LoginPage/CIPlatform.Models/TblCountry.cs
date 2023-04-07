using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CIPlatform.Models;

public partial class TblCountry
{
    public long CountryId { get; set; }

    public string? Name { get; set; }

    public string? Iso { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
    [JsonIgnore]

    public virtual ICollection<TblCity> TblCities { get; } = new List<TblCity>();
    [JsonIgnore]

    public virtual ICollection<TblUser> TblUsers { get; } = new List<TblUser>();
}
