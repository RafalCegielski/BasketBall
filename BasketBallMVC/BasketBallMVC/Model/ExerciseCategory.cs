using System;
using System.Collections.Generic;

namespace BasketBallMVC.Model
{
    public class ExerciseCategory
    {
        public Guid ExerciseCategoryId { get; set; }
        public string Name { get; set; }
        public int Distance { get; set; }

        public virtual List<Exercise> Exercises { get; set; }
    }
}