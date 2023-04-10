using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class ContactU
    {
        public long ContcatId { get; set; }
        public long? UserId { get; set; }
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
