using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeirdFlex.Data.Model
{
    public class TrainingPlanExercise
    {
        public long Id { get; set; }

        public int Order { get; set; }

        public long TrainingPlanId { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; } = null!;

        public long ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; } = null!;
    }
}
