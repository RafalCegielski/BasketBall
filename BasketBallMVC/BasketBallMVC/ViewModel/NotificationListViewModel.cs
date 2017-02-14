using BasketBallMVC.Model;
using PagedList;
using System.Collections.Generic;

namespace BasketBallMVC.ViewModel
{
    public class NotificationListViewModel : LayoutViewModel
    {
        public List<Notification> NotificationList { get; set; }
        public IPagedList<Notification> NotificationListPaged { get; set; }
    }
}