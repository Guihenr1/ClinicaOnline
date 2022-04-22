using System.Collections.Generic;
using System.Linq;

namespace ClinicaOnline.Core.Notification
{
    public class NotificationContext
    {
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public virtual void AddNotification(string key, string message)
        {
            _notifications.Add(new Notification(key, message));
        }
    }
}