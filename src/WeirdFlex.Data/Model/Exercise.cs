using System.Collections.Generic;
using WeirdFlex.Common.Enums;

namespace WeirdFlex.Data.Model
{
    public class Exercise
    {
        public long Id { get; set; }

        public ExerciseType ExerciseType { get; set; }

        public string Name { get; set; }

        public string? Equipment { get; set; }

        public string? ImageRef { get; set; }

        public Exercise(ExerciseType exerciseType, string Name)
        {
            this.ExerciseType = exerciseType;
            this.Name = Name;
        }

        public virtual ICollection<TrainingPlanExercise> PlannedIn { get; set; } = new List<TrainingPlanExercise>();

        public virtual ICollection<ExerciseInstance> PerformedIn { get; set; } = new List<ExerciseInstance>();
    }
}
