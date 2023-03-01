using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class UserSkill
    {
        public long SkillId { get; set; }
        public string? SkillName { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
