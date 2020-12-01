using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeirdFlex.Data.Model
{
    public class ExerciseInstance
    {
        public long Id { get; set; }

        public int Order { get; set; }

        public bool IsReadOnly { get; set; }

        public long ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; } = null!;

        public long TrainingPlanInstanceId { get; set; }
        public virtual TrainingPlanInstance TrainingPlanInstance { get; set; } = null!;

        public virtual ICollection<ExerciseSet> Sets { get; set; } = new List<ExerciseSet>();
    }
}
