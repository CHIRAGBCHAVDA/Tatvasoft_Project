using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CIPlatform.Models
{
    public partial class ApprovalStatus
    {
        public ApprovalStatus()
        {
            Comments = new HashSet<Comment>();
        }

        public byte ApprovalStatusId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
