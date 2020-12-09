using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeirdFlex.Data.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageRef = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPlans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastExecution = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPlanExercises",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    TrainingPlanId = table.Column<long>(type: "bigint", nullable: false),
                    ExerciseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlanExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPlanExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingPlanExercises_TrainingPlans_TrainingPlanId",
                        column: x => x.TrainingPlanId,
                        principalTable: "TrainingPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPlanInstances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainingPlanId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlanInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPlanInstances_TrainingPlans_TrainingPlanId",
                        column: x => x.TrainingPlanId,
                        principalTable: "TrainingPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseInstances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    ExerciseId = table.Column<long>(type: "bigint", nullable: false),
                    TrainingPlanInstanceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseInstances_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseInstances_TrainingPlanInstances_TrainingPlanInstanceId",
                        column: x => x.TrainingPlanInstanceId,
                        principalTable: "TrainingPlanInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Repetitions = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    Distance = table.Column<double>(type: "float", nullable: true),
                    ExerciseInstanceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSets_ExerciseInstances_ExerciseInstanceId",
                        column: x => x.ExerciseInstanceId,
                        principalTable: "ExerciseInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Equipment", "ExerciseType", "ImageRef", "Name" },
                values: new object[,]
                {
                    { 1L, null, 3, null, "Running" },
                    { 29L, "Dumbbell", 1, null, "Hammer Curl" },
                    { 28L, "Dumbbell", 1, null, "One Arm Row" },
                    { 27L, "Dumbbell", 1, null, "Front Raise" },
                    { 26L, "Dumbbell", 1, null, "Lateral Raise" },
                    { 25L, "Dumbbell", 1, null, "Shoulder Press" },
                    { 24L, "Cable", 1, null, "Unilateral High Flye" },
                    { 23L, "Cable", 1, null, "Standing Chest Press" },
                    { 22L, "Cable", 1, null, "Seated Row" },
                    { 21L, "Cable", 1, null, "Triceps Pushdown" },
                    { 20L, "Cable", 1, null, "Face Pull" },
                    { 19L, "Barbell", 1, null, "Bentover Row" },
                    { 18L, "Barbell", 1, null, "Front/Push Press" },
                    { 17L, "Barbell", 1, null, "Deadlift" },
                    { 30L, "Dumbbell", 1, null, "Bench Press" },
                    { 16L, "Barbell", 1, null, "Squats" },
                    { 14L, null, 0, null, "Dips" },
                    { 13L, null, 0, null, "Chin-Up (Regular)" },
                    { 12L, null, 0, null, "Pull-Up (Wide)" },
                    { 11L, null, 0, null, "Hanging Leg Raise" },
                    { 10L, null, 0, null, "Abdominal Reverse Curl" },
                    { 9L, null, 0, null, "Leg Raised Situps" },
                    { 8L, null, 0, null, "Situps (Regular)" },
                    { 7L, null, 2, null, "Hanging" },
                    { 6L, null, 2, null, "Plank" },
                    { 5L, null, 2, null, "Climbing" },
                    { 4L, null, 3, null, "Rowing" },
                    { 3L, null, 3, null, "Cycling" },
                    { 2L, null, 3, null, "Swimming" },
                    { 15L, "Barbell", 1, null, "Bench Press" },
                    { 31L, "Dumbbell", 1, null, "Flye" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseInstances_ExerciseId",
                table: "ExerciseInstances",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseInstances_TrainingPlanInstanceId",
                table: "ExerciseInstances",
                column: "TrainingPlanInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSets_ExerciseInstanceId",
                table: "ExerciseSets",
                column: "ExerciseInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlanExercises_ExerciseId",
                table: "TrainingPlanExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlanExercises_TrainingPlanId",
                table: "TrainingPlanExercises",
                column: "TrainingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlanInstances_TrainingPlanId",
                table: "TrainingPlanInstances",
                column: "TrainingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_UserId",
                table: "TrainingPlans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Uid",
                table: "Users",
                column: "Uid",
                unique: true,
                filter: "[Uid] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseSets");

            migrationBuilder.DropTable(
                name: "TrainingPlanExercises");

            migrationBuilder.DropTable(
                name: "ExerciseInstances");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "TrainingPlanInstances");

            migrationBuilder.DropTable(
                name: "TrainingPlans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
