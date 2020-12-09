using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeirdFlex.Common.Enums;
using WeirdFlex.Data.Model;

namespace WeirdFlex.Data.EF.Migrations
{
    internal static class ExerciseSeed
    {
        public static void Seed(this EntityTypeBuilder<Exercise> builder)
        {
            short id = 0;

            var objects = new Exercise[]
            {
                // Stamina
                new Exercise(ExerciseType.DurationAndDistance, "Running") { Id = ++id },
                new Exercise(ExerciseType.DurationAndDistance, "Swimming") { Id = ++id },
                new Exercise(ExerciseType.DurationAndDistance, "Cycling") { Id = ++id },
                new Exercise(ExerciseType.DurationAndDistance, "Rowing") { Id = ++id },

                new Exercise(ExerciseType.Duration, "Climbing") { Id = ++id },
                new Exercise(ExerciseType.Duration, "Plank") { Id = ++id },
                new Exercise(ExerciseType.Duration, "Hanging") { Id = ++id },

                new Exercise(ExerciseType.Reps, "Situps (Regular)") { Id = ++id },
                new Exercise(ExerciseType.Reps, "Leg Raised Situps") { Id = ++id },
                new Exercise(ExerciseType.Reps, "Abdominal Reverse Curl") { Id = ++id },
                
                new Exercise(ExerciseType.Reps, "Hanging Leg Raise") { Id = ++id },
                
                new Exercise(ExerciseType.Reps, "Pull-Up (Wide)") { Id = ++id },
                new Exercise(ExerciseType.Reps, "Chin-Up (Regular)") { Id = ++id },

                new Exercise(ExerciseType.Reps, "Dips") { Id = ++id },

                // Barbell
                new Exercise(ExerciseType.RepsAndWeight, "Bench Press") { Id = ++id, Equipment = "Barbell" },
                new Exercise(ExerciseType.RepsAndWeight, "Squats") { Id = ++id, Equipment = "Barbell" },
                new Exercise(ExerciseType.RepsAndWeight, "Deadlift") { Id = ++id, Equipment = "Barbell" },
                new Exercise(ExerciseType.RepsAndWeight, "Front/Push Press") { Id = ++id, Equipment = "Barbell" },
                new Exercise(ExerciseType.RepsAndWeight, "Bentover Row") { Id = ++id, Equipment = "Barbell" },

                // Cable
                new Exercise(ExerciseType.RepsAndWeight, "Face Pull") { Id = ++id, Equipment = "Cable" },
                new Exercise(ExerciseType.RepsAndWeight, "Triceps Pushdown") { Id = ++id, Equipment = "Cable" },
                new Exercise(ExerciseType.RepsAndWeight, "Seated Row") { Id = ++id, Equipment = "Cable" },
                new Exercise(ExerciseType.RepsAndWeight, "Standing Chest Press") { Id = ++id, Equipment = "Cable" },
                new Exercise(ExerciseType.RepsAndWeight, "Unilateral High Flye") { Id = ++id, Equipment = "Cable" },


                // Dumbbell
                new Exercise(ExerciseType.RepsAndWeight, "Shoulder Press") { Id = ++id, Equipment = "Dumbbell" },
                new Exercise(ExerciseType.RepsAndWeight, "Lateral Raise") { Id = ++id, Equipment = "Dumbbell" },
                new Exercise(ExerciseType.RepsAndWeight, "Front Raise") { Id = ++id, Equipment = "Dumbbell" },
                new Exercise(ExerciseType.RepsAndWeight, "One Arm Row") { Id = ++id, Equipment = "Dumbbell" },
                new Exercise(ExerciseType.RepsAndWeight, "Hammer Curl") { Id = ++id, Equipment = "Dumbbell" },
                new Exercise(ExerciseType.RepsAndWeight, "Bench Press") { Id = ++id, Equipment = "Dumbbell" },
                new Exercise(ExerciseType.RepsAndWeight, "Flye") { Id = ++id, Equipment = "Dumbbell" },
            };

            builder.HasData(objects);
        }
    }
}
