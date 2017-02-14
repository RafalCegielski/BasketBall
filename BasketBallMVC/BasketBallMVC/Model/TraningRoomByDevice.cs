using System;

namespace BasketBallMVC.Model
{
    public class TraningRoomByDevice
    {
        public Guid TraningRoomByDeviceID { get; set; }
        public DateTime BuyDate { get; set; }
        
        public virtual Device Device { get; set; }
        public virtual TraningRoom TraningRoom { get; set; }
    }
}
