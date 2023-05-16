using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class UserNotification
    {
        public long UserNotificationId { get; set; }
        public long? UserId { get; set; }
        public int? NotificationId { get; set; }
        public string NotificationText { get; set; } = null!;
        public long? NotificationAnchorId { get; set; }
        public DateTime NotificationDate { get; set; }
        public byte? MarkedAsRead { get; set; }

        public virtual Notification? Notification { get; set; }
        public virtual User? User { get; set; }
    }
}
