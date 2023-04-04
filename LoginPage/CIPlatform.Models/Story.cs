﻿using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class Story
    {
        public long StoryId { get; set; }
        public long UserId { get; set; }
        public long MissionId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public byte StoryStatusId { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long? Views { get; set; }
    }
}
