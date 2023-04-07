using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
