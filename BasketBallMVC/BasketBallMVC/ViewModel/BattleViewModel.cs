using BasketBallMVC.Model;
using System.Collections.Generic;

namespace BasketBallMVC.ViewModel
{
    public class BattleViewModel : LayoutViewModel
    {
        public bool isAttacksChosen { get; set; }
        public bool isEnoughLife { get; set; }
        public bool isEnoughEnergy { get; set; }
        public List<BattleLog> battleLog { get; set; }
        public int OponentResult { get; set; }
        public int PlayerResult { get; set; }
        public string OponentNick { get; set; }
        public string PlayerNick { get; set; }
    }
}