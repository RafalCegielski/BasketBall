using BasketBallMVC.Infrastructure;
using System;

namespace BasketBallMVC.Model
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        public NotificationType notificationType { get; set; }
        public string notificationDetails { get; set; }
        public bool isOpen { get; set; }

        public virtual ApplicationUser user { get; set; }
    }

   

}