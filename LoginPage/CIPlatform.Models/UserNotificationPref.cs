using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class UserNotificationPref
    {
        public long PrefId { get; set; }
        public int? NotificationId { get; set; }
        public long? UserId { get; set; }

        public virtual Notification? Notification { get; set; }
        public virtual User? User { get; set; }
    }
}
