using System;
using System.Collections.Generic;

namespace CIPlatform.Models;

public partial class TblCity
{
    public long CityId { get; set; }

    public long? CountryId { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual TblCountry? Country { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; } = new List<TblUser>();
}
