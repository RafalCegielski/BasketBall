using BasketBallMVC.DAL;
using BasketBallMVC.Hubs;
using BasketBallMVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BasketBallMVC.Services
{
    public class MessageService
    {

        public void RemoveMessage(string messageId)
        {
            using (var db = new BasketBallContext())
            {
                var id = new Guid(messageId);
                var message = db.Messages.Find(id);
                db.Messages.Remove(message);
                db.SaveChanges();
            }
        }

        public bool CheckAllMessageStatus()
        {
            using (var db = new BasketBallContext())
            {
                if (db.Messages.Any(x => x.Addressee.Email == HttpContext.Current.User.Identity.Name && x.isRead == false))
                {
                    return false;
                }
                return true;

            }
        }

        public string GetMessageDetails(string messageId)
        {
            string message = "";
            using (var db = new BasketBallContext())
            {
                Guid id = new Guid(messageId);
                var messageObj = db.Messages.Find(id);
                message = messageObj.Details;
                messageObj.isRead = true;
                db.SaveChanges();
            }
            return message;
        }

        public void CreateMessage(string message, string addressee)
        {

            using (var db = new BasketBallContext())
            {

                var addresseeUser = db.Users.Find(addressee);
                var senderUser = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                string title = string.Empty;
                if (message.Length > 22)
                {
                    title = message.Substring(0, 22);
                    title += "...";
                }
                else
                {
                    title = message;
                }

                db.Messages.Add(new Message { Addressee = addresseeUser, Details = message, isRead = false, Sender = senderUser, MessageId = Guid.NewGuid(), SendDate = DateTime.Now, Title = title });
                db.SaveChanges();
                NotificationHub.SendMessage(addresseeUser.Email);
            }
        }
    }
}