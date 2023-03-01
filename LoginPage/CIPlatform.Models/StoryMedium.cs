﻿using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class StoryMedium
    {
        public long StoryMediaId { get; set; }
        public long StoryId { get; set; }
        public string Type { get; set; } = null!;
        public string? Path { get; set; }
        public DateTime? CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
