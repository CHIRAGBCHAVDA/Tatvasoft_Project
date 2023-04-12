using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class ApprovalStatus
    {
        public ApprovalStatus()
        {
            Comments = new HashSet<Comment>();
            Timesheets = new HashSet<Timesheet>();
        }

        public byte ApprovalStatusId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}
