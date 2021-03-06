﻿using WeirdFlex.Common.Enums;

namespace WeirdFlex.Business.Views.ViewModels
{
    public class CreateExerciseModel
    {
        public ExerciseType ExerciseType { get; set; }

        public string Name { get; set; }

        public string? ImageRef { get; set; }

        public CreateExerciseModel(ExerciseType exerciseType, string name, string imageRef)
        {
            this.ExerciseType = exerciseType;
            this.Name = name;
            this.ImageRef = imageRef;
        }
    }
}
