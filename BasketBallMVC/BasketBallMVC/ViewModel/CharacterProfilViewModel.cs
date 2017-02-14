using BasketBallMVC.Model;
using System.Collections.Generic;

namespace BasketBallMVC.ViewModel
{
    public class CharacterProfilViewModel : LayoutViewModel
    {
        public List<Device> RecentlyPurchasedDevices { get; set; }
        public List<ExerciseViewModel> RecentlyTrainedExercises { get; set; }
        public int Strengh { get; set; }
        public int Marksmanship { get; set; }
        public int Speed { get; set; }
        public int Defence { get; set; }
        public int WinGames { get; set; }
        public int LoseGames { get; set; }
        public int GameCounter { get; set; }
        public int RankingPosition { get; set; }
        public string TargetUserName { get; set; }
    }
}