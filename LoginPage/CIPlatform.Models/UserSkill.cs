﻿using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class UserSkill
    {
        public long UserSkillId { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long? UserId { get; set; }
        public long? SkillId { get; set; }

        public virtual Skill? Skill { get; set; }
        public virtual User? User { get; set; }
    }
}
