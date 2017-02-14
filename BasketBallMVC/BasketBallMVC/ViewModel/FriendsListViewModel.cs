using PagedList;
using System.Collections.Generic;

namespace BasketBallMVC.ViewModel
{
    public class FriendsListViewModel : LayoutViewModel
    {
        public List<CharacterForFriendList> CharacterForFriendList { get; set; }
        public IPagedList<CharacterForFriendList> CharacterForFriendListPaged { get; set; }
    }
    public class CharacterForFriendList
    {
        public bool isAvalibleToAttack { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
    }

}