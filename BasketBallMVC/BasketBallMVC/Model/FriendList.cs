using System;

namespace BasketBallMVC.Model
{
    public class FriendList
    {
        public Guid FriendListID { get; set; }

        public string InvitingUserEmail { get; set; }
        public string InvitedUserEmail { get; set; }
    }
}
