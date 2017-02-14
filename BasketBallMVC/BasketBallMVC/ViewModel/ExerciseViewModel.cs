using BasketBallMVC.Model;
using System;
using System.Collections.Generic;

namespace BasketBallMVC.ViewModel
{
    public class ExerciseViewModel
    {
        public Guid ExerciseID { get; set; }
        public int Time { get; set; }
        public int Experience { get; set; }
        public int Value { get; set; }
        public int Level { get; set; }
        public int Cost { get; set; }
        public int Energy { get; set; }
        public bool isAtack { get; set; }

        public virtual List<TraningRoomByExercise> TraningRoomByExercises { get; set; }
        public virtual ExerciseCategory ExerciseCategory { get; set; }
    }
}