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

        public int Order { get; set; }

        public TrainingPlan(long userId, string name)
        {
            this.UserId = userId;
            this.Name = name;
        }

        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<TrainingPlanInstance> Executions { get; set; } = new List<TrainingPlanInstance>();

        public virtual ICollection<TrainingPlanExercise> PlannedExercises { get; set; } = new List<TrainingPlanExercise>();
    }
}
