﻿using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class Skill
    {
        public long SkillId { get; set; }
        public string? SkillName { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
