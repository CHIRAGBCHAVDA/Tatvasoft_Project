using System;
using System.Collections.Generic;

namespace CIPlatform.Models;

public partial class TblCountry
{
    public long CountryId { get; set; }

    public string? Name { get; set; }

    public string? Iso { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<TblCity> TblCities { get; } = new List<TblCity>();

    public virtual ICollection<TblUser> TblUsers { get; } = new List<TblUser>();
}
