using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class PasswordReset
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
