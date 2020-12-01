using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeirdFlex.Common.Enums;

namespace WeirdFlex.Business.Views.ViewModels
{
    public class CreateExerciseModel
    {
        public ExerciseType ExerciseType { get; set; }

        public string Name { get; set; }

        public string? ImageRef { get; set; }
    }
}
