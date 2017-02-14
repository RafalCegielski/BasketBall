using System;

namespace BasketBallMVC.Model
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime SendDate { get; set; }
        public bool isRead { get; set; }

        public virtual ApplicationUser Sender { get; set; }
        public virtual ApplicationUser Addressee { get; set; }
    }
}