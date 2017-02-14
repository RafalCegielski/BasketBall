using BasketBallMVC.DAL;
using BasketBallMVC.Infrastructure;
using BasketBallMVC.Model;
using System;
using System.Linq;
using System.Web;

namespace BasketBallMVC.Services
{
    public class NotificationService
    {
        public void AddInviteNotification(string invitedEmail, string invitingEmail)
        {
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == invitedEmail);
                db.Notifications.Add(new Notification { isOpen = false, notificationDetails = Consts.ZaproszenieDoZajomychOd + invitingEmail, NotificationId = Guid.NewGuid(), notificationType = NotificationType.Zaproszenie, user = user });
                db.SaveChanges();
            }
        }

        public void AddBattleNotification(string attacked)
        {
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == attacked);
                db.Notifications.Add(new Notification { isOpen = false, notificationDetails = Consts.ZaatakowanyPrzez + HttpContext.Current.User.Identity.Name, NotificationId = Guid.NewGuid(), notificationType = NotificationType.Atak, user = user });
                db.SaveChanges();
            }
        }

        public void DeleteNotifiction(string id)
        {
            using (var db = new BasketBallContext())
            {
                Guid idGuid = new Guid(id);
                var notification = db.Notifications.Find(idGuid);
                db.Notifications.Remove(notification);
                db.SaveChanges();
            }
        }
    }
}