using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig121 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfDoors",
                table: "ChurchGoals",
                newName: "NumberOfResidentsInGoal");

            migrationBuilder.RenameColumn(
                name: "NumberOfConnections",
                table: "ChurchGoals",
                newName: "NumberOfDoorsHangarsLeft");

            migrationBuilder.AddColumn<int>(
                name: "ChurchGoalId",
                table: "Missions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfConnectionsMade",
                table: "ChurchGoals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChurchGoalId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "NumberOfConnectionsMade",
                table: "ChurchGoals");

            migrationBuilder.RenameColumn(
                name: "NumberOfResidentsInGoal",
                table: "ChurchGoals",
                newName: "NumberOfDoors");

            migrationBuilder.RenameColumn(
                name: "NumberOfDoorsHangarsLeft",
                table: "ChurchGoals",
                newName: "NumberOfConnections");
        }
    }
}
