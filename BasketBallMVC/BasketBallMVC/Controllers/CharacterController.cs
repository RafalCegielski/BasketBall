using BasketBallMVC.Services;
using BasketBallMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;
using BasketBallMVC.Hubs;

namespace BasketBallMVC.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private UserService _userService = new UserService();
        private ComplementViewModelsService _complementVMService = new ComplementViewModelsService();
        private FriendService _friendService = new FriendService();
        private NotificationService _notificationService = new NotificationService();
        // GET: Character
        public ActionResult FriendsList(int? Page)
        {
            int page = Page ?? 1;
            FriendsListViewModel firendsVM = _complementVMService.ComplementFriendsListVM();

            if (firendsVM.CharacterForFriendList != null)
            {
                firendsVM.CharacterForFriendList =  firendsVM.CharacterForFriendList.OrderBy(x => x.Nick).ToList();
                firendsVM.CharacterForFriendListPaged = firendsVM.CharacterForFriendList.ToPagedList(page, 11);
            }

            return View(firendsVM);
        }

        public ActionResult Ranking(int? Page, string searchString)
        {
            int page = Page ?? 1;
            RankingViewModel rankingVM = _complementVMService.ComplementRankingVM();

            if (rankingVM.CharacterForRanking != null)
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    rankingVM.CharacterForRanking = rankingVM.CharacterForRanking.Where(s => s.Nick.Contains(searchString)).ToList();
                }
                rankingVM.CharacterForRanking = rankingVM.CharacterForRanking.OrderByDescending(x => x.TotalPoints).ToList();
                rankingVM.CharacterForRankingPaged = rankingVM.CharacterForRanking.ToPagedList(page, 11);
            }
            return View(rankingVM);
        }
        public JsonResult CreateFriendInvitation(string invitedEmail)
        {
            var AllCurrentUserFriendInvitation = _userService.GetCurrentUserAllFriendInvitation();
            var AllCurrentUserFriends = _userService.GetCurrentUserAllFriends();
            var result = _friendService.InviteFriend(AllCurrentUserFriendInvitation, AllCurrentUserFriends, invitedEmail);
            Dictionary<string, string> dictionaryResult = new Dictionary<string, string>();

            string json = string.Empty;

            switch (result)
            {
                case "UserException":
                    dictionaryResult.Add("status", "UserException");
                    break;
                case "InvitationException":
                    dictionaryResult.Add("status", "InvitationException");
                    break;
                case "FriendException":
                    dictionaryResult.Add("status", "FriendException");
                    break;
                case "Success":
                    dictionaryResult.Add("status", "Success");
                    _notificationService.AddInviteNotification(invitedEmail, User.Identity.Name);
                    NotificationHub.AddNotification(invitedEmail);
                    break;
                case "SelfException":
                    dictionaryResult.Add("status", "SelfException");
                    break;
            }
            json = JsonConvert.SerializeObject(dictionaryResult);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public void RemoveFriend(string friendNick)
        {
            _friendService.RemoveFriend(friendNick);
        }
       
        public ActionResult CharacterProfil(string nick)
        {
            var userId = _userService.GetUserIdByNick(nick);
            CharacterProfilViewModel characterProfileVM = _complementVMService.ComplementCharacterProfilVM(userId);
            
            return View(characterProfileVM);
        }
    }
}