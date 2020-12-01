﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeirdFlex.Data.EF;

namespace WeirdFlex.Data.EF.Migrations
{
    [DbContext(typeof(FlexContext))]
    partial class FlexContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WeirdFlex.Data.Model.Exercise", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("ExerciseType")
                        .HasColumnType("int");

                    b.Property<string>("ImageRef")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.ExerciseInstance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ExerciseId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<long>("TrainingPlanInstanceId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("TrainingPlanInstanceId");

                    b.ToTable("ExerciseInstances");
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.ExerciseSet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<double?>("Distance")
                        .HasColumnType("double");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<long>("ExerciseInstanceId")
                        .HasColumnType("bigint");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("Repetitions")
                        .HasColumnType("int");

                    b.Property<double?>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseInstanceId");

                    b.ToTable("ExerciseSets");
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.TrainingPlan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ImageRef")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("LastExecution")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TrainingPlans");
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.TrainingPlanExercise", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ExerciseId")
                        .HasColumnType("bigint");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<long>("TrainingPlanId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("TrainingPlanId");

                    b.ToTable("TrainingPlanExercises");
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.TrainingPlanInstance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("TrainingPlanId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TrainingPlanId");

                    b.ToTable("TrainingPlanInstances");
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.ExerciseInstance", b =>
                {
                    b.HasOne("WeirdFlex.Data.Model.Exercise", "Exercise")
                        .WithMany("PerformedIn")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WeirdFlex.Data.Model.TrainingPlanInstance", "TrainingPlanInstance")
                        .WithMany("PerformedExercises")
                        .HasForeignKey("TrainingPlanInstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.ExerciseSet", b =>
                {
                    b.HasOne("WeirdFlex.Data.Model.ExerciseInstance", "ExerciseInstance")
                        .WithMany("Sets")
                        .HasForeignKey("ExerciseInstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.TrainingPlan", b =>
                {
                    b.HasOne("WeirdFlex.Data.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.TrainingPlanExercise", b =>
                {
                    b.HasOne("WeirdFlex.Data.Model.Exercise", "Exercise")
                        .WithMany("PlannedIn")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WeirdFlex.Data.Model.TrainingPlan", "TrainingPlan")
                        .WithMany("PlannedExercises")
                        .HasForeignKey("TrainingPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WeirdFlex.Data.Model.TrainingPlanInstance", b =>
                {
                    b.HasOne("WeirdFlex.Data.Model.TrainingPlan", "TrainingPlan")
                        .WithMany("Executions")
                        .HasForeignKey("TrainingPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
