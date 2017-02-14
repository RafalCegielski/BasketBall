using System;

namespace BasketBallMVC.ViewModel
{
    public class LayoutViewModel
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int Energy { get; set; }
        public int Health { get; set; }
        public int HealthInPercent { get; set; }
        public int EnergyInPercent { get; set; }
        public int LevelInPercent { get; set; }
        public int MaxHealth { get; set; }
        public int MaxEnergy { get; set; }
        public int MaxLevel { get; set; }
        public bool Notifications { get; set; }
        public bool Messages { get; set; }
        public bool IsBusy { get; set; }
        public DateTime BusyEndTime { get; set; }

        public LayoutViewModel()
        {
            MaxLevel = 40;
        }
    }
}