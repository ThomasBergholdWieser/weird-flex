using System;

namespace WeirdFlex.Data.Model
{
    public class ExerciseSet
    {
        public long Id { get; set; }

        public ExerciseSet(long exerciseInstanceId)
        {
            this.ExerciseInstanceId = exerciseInstanceId;
        }

        public int Order { get; set; }

        public int? Repetitions { get; set; }

        public double? Weight { get; set; }

        public TimeSpan? Duration { get; set; }

        public double? Distance { get; set; }

        public long ExerciseInstanceId { get; set; }
        public virtual ExerciseInstance ExerciseInstance { get; set; } = null!;
    }
}
