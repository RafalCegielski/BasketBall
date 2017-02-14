namespace BasketBallMVC.ViewModel
{
    public class RestaurantViewModel : LayoutViewModel
    {
        public bool isSmallAvailible { get; set; }
        public bool isMediumAvailible { get; set; }
        public bool isBigAvailible { get; set; }

        public int SmallCost { get; set; }
        public int MediumCost { get; set; }
        public int BigCost { get; set; }
    }
}