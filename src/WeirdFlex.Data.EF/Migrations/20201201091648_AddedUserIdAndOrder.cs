using Microsoft.EntityFrameworkCore.Migrations;

namespace WeirdFlex.Data.EF.Migrations
{
    public partial class AddedUserIdAndOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TrainingPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "TrainingPlans",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ExerciseInstanceId",
                table: "ExerciseSets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_UserId",
                table: "TrainingPlans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSets_ExerciseInstanceId",
                table: "ExerciseSets",
                column: "ExerciseInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSets_ExerciseInstances_ExerciseInstanceId",
                table: "ExerciseSets",
                column: "ExerciseInstanceId",
                principalTable: "ExerciseInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_Users_UserId",
                table: "TrainingPlans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSets_ExerciseInstances_ExerciseInstanceId",
                table: "ExerciseSets");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_Users_UserId",
                table: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlans_UserId",
                table: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseSets_ExerciseInstanceId",
                table: "ExerciseSets");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "TrainingPlans");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrainingPlans");

            migrationBuilder.DropColumn(
                name: "ExerciseInstanceId",
                table: "ExerciseSets");
        }
    }
}
