using System;

namespace BasketBallMVC.Model
{
    public class Level
    {
        public Guid LevelId { get; set; }
        public int Lvl { get; set; }
        public int MaxHealth { get; set; }
        public int MaxEnergy { get; set; }
        public int MaxExperience { get; set; }
    }
}