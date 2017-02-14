using PagedList;
using System;
using System.Collections.Generic;

namespace BasketBallMVC.ViewModel
{
    public class MessageListViewModel : LayoutViewModel
    {
        public List<MessageVM> MessageList { get; set; }
        public IPagedList<MessageVM> MessageListPaged { get; set; }
        public bool HasFriends { get; set; }

    }
    public class MessageVM
    {
        public Guid MessageId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime SendDate { get; set; }
        public string Sender { get; set; }
        public bool isRead { get; set; }
    }
    
}