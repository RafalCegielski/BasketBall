using BasketBallMVC.DAL;
using BasketBallMVC.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketBallMVC.Services
{
    public class UserService
    {
        public string GetUserIdByNick(string nick)
        {
            string id = string.Empty;
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.UserData.Nick == nick);
                id = user.Id;
            }
            return id;
        }

        public string GetUserNickById(string id)
        {
            string nick = string.Empty;
            using (var db = new BasketBallContext())
            {
                var user = db.Users.Find(id);
                nick = user.UserData.Nick;
            }
            return nick;
        }

        public string GetUserNickByIdentityName()
        {
            string nick = string.Empty;
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                nick = user.UserData.Nick;
            }
            return nick;
        }

        public ApplicationUser GetUserFromEmail(string email)
        {
            ApplicationUser user = new ApplicationUser();
            using (var db = new BasketBallContext())
            {
                user = db.Users.FirstOrDefault(x => x.Email == email);
            }
            return user;
        }

        public List<FriendInvitation> GetCurrentUserAllFriendInvitation()
        {
            List<FriendInvitation> allCurrentUserFriendInvitation = new List<FriendInvitation>();
            using (var db = new BasketBallContext())
            {
                allCurrentUserFriendInvitation = db.FriendsInvitation.Where(x => x.InvitingUserEmail == HttpContext.Current.User.Identity.Name || x.InvitedUserEmail == HttpContext.Current.User.Identity.Name).ToList();
            }

            return allCurrentUserFriendInvitation;
        }

        public Character GetCurrentUserCharacter()
        {
            Character character = new Character();
            using (var db = new BasketBallContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name);
                character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
            }
            return character;
        }

        public List<FriendList> GetCurrentUserAllFriends()
        {
            List<FriendList> allCurrentUserFriends = new List<FriendList>();
            using (var db = new BasketBallContext())
            {
                allCurrentUserFriends = db.FriendsList.Where(x => x.InvitingUserEmail == HttpContext.Current.User.Identity.Name || x.InvitedUserEmail == HttpContext.Current.User.Identity.Name).ToList();
            }

            return allCurrentUserFriends;
        }

        public string GetUserEmailById(string id)
        {
            string email = string.Empty;
            using (var db = new BasketBallContext())
            {
                var oponentUser = db.Users.Find(id);
                email = oponentUser.Email;
            }

            return email;
        }
    }
}