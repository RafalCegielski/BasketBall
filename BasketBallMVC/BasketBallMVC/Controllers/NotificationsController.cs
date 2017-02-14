using BasketBallMVC.Services;
using BasketBallMVC.ViewModel;
using PagedList;
using System.Web.Mvc;

namespace BasketBallMVC.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private ComplementViewModelsService _complementVMService = new ComplementViewModelsService();
        private FriendService _friendService = new FriendService();
        private NotificationService _notificationService = new NotificationService();
        // GET: Notifications
        public ActionResult NotificationList(int? Page)
        {
            int page = Page ?? 1;

            NotificationListViewModel notificationListVM = _complementVMService.ComplementNotificationListVM();
            if (notificationListVM.NotificationList != null)
                notificationListVM.NotificationListPaged = notificationListVM.NotificationList.ToPagedList(page, 11);

            return View(notificationListVM);
        }

        public void AcceptRejectInvitation(string id)
        {
            if (id.Contains("Accept"))
            {
                string inviteId = id.Replace("AcceptInvitation_", "");
                _friendService.AcceptFriendInvitation(inviteId);
            }
            else if (id.Contains("Reject"))
            {
                string inviteId = id.Replace("RejectInvitation_", "");
                _friendService.RejectFriendInvitation(inviteId);
            }
        }

        public void DeleteNotification(string id)
        {
            id = id.Replace("DeleteNotification_", "");
            _notificationService.DeleteNotifiction(id);
        }
    }
}