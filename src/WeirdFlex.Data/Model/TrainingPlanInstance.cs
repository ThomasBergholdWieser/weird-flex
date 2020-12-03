using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeirdFlex.Data.Model
{
    public class TrainingPlanInstance
    {
        public long Id { get; set; }

        public TrainingPlanInstance(long trainingPlanId)
        {
            this.TrainingPlanId = trainingPlanId;
        }

        public DateTime StartedAt { get; set; }

        public long TrainingPlanId { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; } = null!;

        public virtual ICollection<ExerciseInstance> PerformedExercises { get; set; } = new List<ExerciseInstance>();
    }
}
