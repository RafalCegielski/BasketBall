using PagedList;
using System.Collections.Generic;

namespace BasketBallMVC.ViewModel
{
    public class RankingViewModel : LayoutViewModel
    {
        public List<CharacterForRanking> CharacterForRanking { get; set; }
        public IPagedList<CharacterForRanking> CharacterForRankingPaged { get; set; }
    }
    public class CharacterForRanking
    {
        public bool isAvalibleToBattle { get; set; }
        public int TotalPoints { get; set; }
        public string Nick { get; set; }
        public int PositionInRanking { get; set; }
    }
}