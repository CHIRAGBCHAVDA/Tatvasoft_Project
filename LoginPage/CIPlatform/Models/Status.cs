using System;
using System.Collections.Generic;

namespace CIPlatform.Models;

public partial class Status
{
    public int Id { get; set; }

    public string? StatusText { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; } = new List<TblUser>();
}
