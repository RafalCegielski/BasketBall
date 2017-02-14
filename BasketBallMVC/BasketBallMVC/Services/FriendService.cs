using BasketBallMVC.DAL;
using BasketBallMVC.Infrastructure;
using BasketBallMVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketBallMVC.Services
{
    public class FriendService
    {
        public void RemoveFriend(string friendNick)
        {
            using (var db = new BasketBallContext())
            {
                var friend = db.Users.FirstOrDefault(x => x.UserData.Nick == friendNick);
                var friendRelation = db.FriendsList.FirstOrDefault(x => (x.InvitedUserEmail == HttpContext.Current.User.Identity.Name && x.InvitingUserEmail == friend.Email) ||
                (x.InvitingUserEmail == HttpContext.Current.User.Identity.Name && x.InvitedUserEmail == friend.Email));
                db.FriendsList.Remove(friendRelation);
                db.SaveChanges();
            }
        }

        public void RejectFriendInvitation(string invitationId)
        {
            using (var db = new BasketBallContext())
            {
                Guid id = new Guid(invitationId);
                var notification = db.Notifications.Find(id);
                string invitingUserEmail = notification.notificationDetails.Replace(Consts.ZaproszenieDoZajomychOd, "");
                var invitation = db.FriendsInvitation.FirstOrDefault(x => (x.InvitedUserEmail == invitingUserEmail && x.InvitingUserEmail == notification.user.Email) ||
                                                                          (x.InvitedUserEmail == notification.user.Email && x.InvitingUserEmail == invitingUserEmail));
                db.Notifications.Remove(notification);
                db.FriendsInvitation.Remove(invitation);
                db.SaveChanges();
            }
        }

        public string InviteFriend(List<FriendInvitation> allCurrentUserFriendInvitation, List<FriendList> allCurrentUserFriends, string invitedEmail)
        {
            string identityName = HttpContext.Current.User.Identity.Name;
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == invitedEmail);
                if (user == null)
                    return "UserException";
            }

            if (invitedEmail == identityName)
            {
                return "SelfException";
            }

            if (allCurrentUserFriendInvitation == null ||
                allCurrentUserFriendInvitation.Any(x => x.InvitedUserEmail == invitedEmail && x.InvitingUserEmail == identityName) ||
                allCurrentUserFriendInvitation.Any(x => x.InvitedUserEmail == identityName && x.InvitingUserEmail == invitedEmail))
            {
                return "InvitationException";
            }
            else
            if (allCurrentUserFriends == null ||
                allCurrentUserFriends.Any(x => x.InvitedUserEmail == identityName && x.InvitingUserEmail == invitedEmail) ||
                allCurrentUserFriends.Any(x => x.InvitedUserEmail == invitedEmail && x.InvitingUserEmail == identityName))
            {
                return "FriendException";
            }
            else
            {
                using (var db = new BasketBallContext())
                {
                    db.FriendsInvitation.Add(new FriendInvitation { InvitedUserEmail = invitedEmail, InvitingUserEmail = identityName, FriendInvitationId = Guid.NewGuid() });
                    db.SaveChanges();
                }

                return "Success";
            }
        }

        public void AcceptFriendInvitation(string invitationId)
        {
            using (var db = new BasketBallContext())
            {
                Guid id = new Guid(invitationId);
                var notification = db.Notifications.Find(id);
                string invitingUserEmail = notification.notificationDetails.Replace(Consts.ZaproszenieDoZajomychOd, "");
                db.FriendsList.Add(new FriendList { FriendListID = Guid.NewGuid(), InvitedUserEmail = notification.user.Email, InvitingUserEmail = invitingUserEmail });
                var invitation = db.FriendsInvitation.FirstOrDefault(x => (x.InvitedUserEmail == invitingUserEmail && x.InvitingUserEmail == notification.user.Email) ||
                                                                          (x.InvitedUserEmail == notification.user.Email && x.InvitingUserEmail == invitingUserEmail));
                db.Notifications.Remove(notification);
                db.FriendsInvitation.Remove(invitation);
                db.SaveChanges();
            }
        }
    }
}