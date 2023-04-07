using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CIPlatform.Models;

public partial class Status
{
    public int Id { get; set; }

    public string? StatusText { get; set; }
    [JsonIgnore]
    public virtual ICollection<TblUser> TblUsers { get; } = new List<TblUser>();
}
