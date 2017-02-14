using System;

namespace BasketBallMVC.Model
{
    public class FriendInvitation
    {
        public Guid FriendInvitationId { get; set; }

        public string InvitingUserEmail { get; set; }
        public string InvitedUserEmail { get; set; }
    }
}