using System.Collections.Generic;

namespace BasketBallMVC.ViewModel
{
    public class TrainingRoomViewModel : LayoutViewModel
    {
        public List<Categories> Category { get; set; }
    }
    public class Categories
    {
        public string CategoryName { get; set; }
        public int CategoryDistance { get; set; }
    }
}