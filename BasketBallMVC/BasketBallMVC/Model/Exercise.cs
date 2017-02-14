using System;
using System.Collections.Generic;

namespace BasketBallMVC.Model
{
    public class Exercise
    {
        public Guid ExerciseID { get; set; }
        public int Time { get; set; }
        public int Experience { get; set; }
        public int Value { get; set; }
        public int Level { get; set; }
        public int Cost { get; set; }
        public int Energy { get; set; }

        public virtual List<TraningRoomByExercise> TraningRoomByExercises { get; set; }
        public virtual ExerciseCategory ExerciseCategory { get; set; }
    }
}
