using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class Skill
    {
        public Skill()
        {
            UserSkills = new HashSet<UserSkill>();
        }

        public long SkillId { get; set; }
        public string? SkillName { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
