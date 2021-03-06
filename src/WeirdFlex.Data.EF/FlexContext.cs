﻿using Microsoft.EntityFrameworkCore;
using WeirdFlex.Data.EF.Migrations;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Uid)
                .IsUnique(true);

            base.OnModelCreating(modelBuilder);

            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().Seed();
        }
    }
}
