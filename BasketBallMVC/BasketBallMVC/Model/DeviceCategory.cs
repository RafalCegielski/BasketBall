using System;
using System.Collections.Generic;

namespace BasketBallMVC.Model
{
    public class DeviceCategory
    {
        public Guid DeviceCategoryId { get; set; }
        public string Name { get; set; }
        public int deviceMarker { get; set; }

        public virtual ShopCategory ShopCategory { get; set; }
        public virtual List<Device> Devices { get; set; }
    }
}