using System;

namespace BasketBallMVC.Model
{
    public class Character
    {
        public Guid CharacterID { get; set; }
        public int Strengh { get; set; }
        public int Marksmanship { get; set; }
        public int Defence { get; set; }
        public int Experience { get; set; }
        public int WinGames { get; set; }
        public int LoseGames { get; set; }
        public int GameCounter { get; set; }
        public string UserId { get; set; }
        public int Gold { get; set; }
        public int Energy { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int RankingPosition { get; set; }
        public bool IsBusy { get; set; }
        public DateTime BusyEndTime { get; set; }
        public int gameStyle { get; set; }
        
        public virtual TraningRoom TraningRoom { get; set; }
        public virtual ChosenAttacks chosenAttacks { get; set; }
    }
}
