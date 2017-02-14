using System;
using System.Collections.Generic;

namespace BasketBallMVC.Model
{
    public class Device
    {
        public Guid DeviceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        
        public virtual DeviceCategory DeviceCategory { get; set; }
        public virtual List<TraningRoomByDevice> TraningRoomByDevices { get; set; }
    }
}
