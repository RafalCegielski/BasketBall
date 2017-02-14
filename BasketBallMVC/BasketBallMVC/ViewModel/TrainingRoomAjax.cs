using BasketBallMVC.Model;

namespace BasketBallMVC.ViewModel
{
    public class TrainingRoomAjax
    {
        public Exercise Atack { get; set; }
        public Exercise Defence { get; set; }
        public string ExerciseName { get; set; }
        public bool isAtackCurrentlyTrained { get; set; }
        public bool isDefenceCurrentlyTrained { get; set; }
    }
}