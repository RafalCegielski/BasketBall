using BasketBallMVC.Services;
using BasketBallMVC.ViewModel;
using Newtonsoft.Json;
using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BasketBallMVC.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private UserService _userService = new UserService();
        private ComplementViewModelsService _complementVMService = new ComplementViewModelsService();
        private MessageService _messageService = new MessageService();
        // GET: Messages
        public ActionResult MessageList(int? Page)
        {
            int page = Page ?? 1;
            var messageListVM = _complementVMService.ComplementMessageListVM();

            if (messageListVM.MessageList != null)
            {
                messageListVM.MessageListPaged = messageListVM.MessageList.ToPagedList(page, 9);
            }


            return View(messageListVM);
        }
        public JsonResult MessageDetails(string messageId)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (messageId != null && messageId != null)
            {
                var message = _messageService.GetMessageDetails(messageId);
                dictionary.Add("message", message);
            }

            return Json(dictionary, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FriendList()
        {
            List<MessageFriendList> friends = new List<MessageFriendList>();

            var friendList = _userService.GetCurrentUserAllFriends();

            foreach (var item in friendList)
            {
                if (item.InvitedUserEmail == User.Identity.Name)
                {
                    var user = _userService.GetUserFromEmail(item.InvitingUserEmail);
                    friends.Add(new MessageFriendList { FriendId = user.Id, Nick = user.UserData.Nick });
                }
                else if (item.InvitingUserEmail == User.Identity.Name)
                {
                    var user = _userService.GetUserFromEmail(item.InvitedUserEmail);
                    friends.Add(new MessageFriendList { FriendId = user.Id, Nick = user.UserData.Nick });
                }
            }

            var json = JsonConvert.SerializeObject(friends);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public void SendMessage(string message, string addressee)
        {
            _messageService.CreateMessage(message, addressee);
        }

        public void SendMessageAddresseeFromList(string message, string addressee)
        {
            if (addressee.Contains("AdminMessageTo_"))
                addressee = addressee.Replace("AdminMessageTo_", "");
            else
            {
                var addsresseeNick = addressee.Replace("messageTo_", "");
                addressee = _userService.GetUserIdByNick(addsresseeNick);
            }

            _messageService.CreateMessage(message, addressee);
        }

        public string RemoveMessage(string messageId)
        {
            messageId = messageId.Replace("RemoveMessage_", "");
            _messageService.RemoveMessage(messageId);

            return string.Empty;
        }

        public JsonResult CheckAllMessageStatus()
        {
            Dictionary<string, string> dictionaryStatus = new Dictionary<string, string>();
            bool isAllRead = _messageService.CheckAllMessageStatus();
            dictionaryStatus.Add("status", isAllRead.ToString());

            return Json(dictionaryStatus, JsonRequestBehavior.AllowGet);
        }

    }
}