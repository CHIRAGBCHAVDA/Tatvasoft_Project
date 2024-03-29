﻿using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class CmsPage
    {
        public long CmsPageId { get; set; }
        public string Title { get; set; } = null!;
        public string? Desciption { get; set; }
        public string Slug { get; set; } = null!;
        public bool? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
