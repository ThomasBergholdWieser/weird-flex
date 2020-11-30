using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeirdFlex.Data.Model
{
    public class TrainingPlan
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? ImageRef { get; set; }

        public DateTime? LastExecution { get; set; }

        public TrainingPlan(string name)
        {
            this.Name = name;
        }

        public virtual ICollection<TrainingPlanInstance> Executions { get; set; } = new List<TrainingPlanInstance>();

        public virtual ICollection<TrainingPlanExercise> PlannedExercises { get; set; } = new List<TrainingPlanExercise>();
    }
}
