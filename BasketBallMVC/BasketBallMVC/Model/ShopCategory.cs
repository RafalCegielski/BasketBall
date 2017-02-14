using System;
using System.Collections.Generic;

namespace BasketBallMVC.Model
{
    public class ShopCategory
    {
        public Guid ShopCategoryId { get; set; }
        public string Name { get; set; }

        public virtual List<DeviceCategory> DeviceCategories { get; set; }
    }
}