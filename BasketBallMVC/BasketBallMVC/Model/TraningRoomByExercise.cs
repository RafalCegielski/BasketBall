using System;

namespace BasketBallMVC.Model
{
    public class TraningRoomByExercise
    {
        public Guid TraningRoomByExerciseID { get; set; }
        public bool IsCompleteAtack { get; set; }
        public bool IsCompleteDefence { get; set; }
        public string JobId { get; set; } 
        public string ExerciseCategoryName { get; set; }
        public DateTime AtackTrainingFinish { get; set; }
        public DateTime DefenceTrainingFinish { get; set; }

        public virtual TraningRoom TraningRoom { get; set; }
        public virtual Exercise ExerciseDefence { get; set; }
        public virtual Exercise ExerciseAtack { get; set; }
    }
}
