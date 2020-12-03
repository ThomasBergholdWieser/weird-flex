using Microsoft.EntityFrameworkCore;
using WeirdFlex.Data.Model;

namespace WeirdFlex.Data.EF
{
    public class FlexContext : DbContext
    {
        public FlexContext(DbContextOptions<FlexContext> options)
           : base(options)
        {
        }

        public DbSet<Exercise> Exercises => Set<Exercise>();

        public DbSet<ExerciseInstance> ExerciseInstances => Set<ExerciseInstance>();

        public DbSet<ExerciseSet> ExerciseSets => Set<ExerciseSet>();

        public DbSet<TrainingPlan> TrainingPlans => Set<TrainingPlan>();

        public DbSet<TrainingPlanExercise> TrainingPlanExercises => Set<TrainingPlanExercise>();

        public DbSet<TrainingPlanInstance> TrainingPlanInstances => Set<TrainingPlanInstance>();

        public DbSet<User> Users => Set<User>();
    }
}
