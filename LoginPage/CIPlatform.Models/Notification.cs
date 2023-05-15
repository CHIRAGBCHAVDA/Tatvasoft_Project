using System;
using System.Collections.Generic;

namespace CIPlatform.Models
{
    public partial class Notification
    {
        public Notification()
        {
            UserNotificationPrefs = new HashSet<UserNotificationPref>();
            UserNotifications = new HashSet<UserNotification>();
        }

        public int NotificationId { get; set; }
        public string NotificationName { get; set; } = null!;

        public virtual ICollection<UserNotificationPref> UserNotificationPrefs { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
    }
}
