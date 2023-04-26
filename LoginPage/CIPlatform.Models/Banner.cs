using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class Banner
    {
        public string Image { get; set; } = null!;
        public string Heading { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long BannerId { get; set; }
    }
}
