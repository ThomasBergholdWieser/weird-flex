﻿using System.Collections.Generic;

namespace WeirdFlex.Data.Model
{
    public class ExerciseInstance
    {
        public long Id { get; set; }

        public ExerciseInstance(long exerciseId, long trainingPlanInstanceId)
        {
            this.ExerciseId = exerciseId;
            this.TrainingPlanInstanceId = trainingPlanInstanceId;
        }

        public int Order { get; set; }

        public bool IsReadOnly { get; set; }

        public long ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; } = null!;

        public long TrainingPlanInstanceId { get; set; }
        public virtual TrainingPlanInstance TrainingPlanInstance { get; set; } = null!;

        public virtual ICollection<ExerciseSet> Sets { get; set; } = new List<ExerciseSet>();
    }
}
