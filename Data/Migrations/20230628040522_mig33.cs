using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residents_Missions_MissionId1",
                table: "Residents");

            migrationBuilder.DropIndex(
                name: "IX_Residents_MissionId1",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "MissionId1",
                table: "Residents");

            migrationBuilder.AlterColumn<int>(
                name: "MissionId",
                table: "Residents",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_MissionId",
                table: "Residents",
                column: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Residents_Missions_MissionId",
                table: "Residents",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residents_Missions_MissionId",
                table: "Residents");

            migrationBuilder.DropIndex(
                name: "IX_Residents_MissionId",
                table: "Residents");

            migrationBuilder.AlterColumn<string>(
                name: "MissionId",
                table: "Residents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MissionId1",
                table: "Residents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Residents_MissionId1",
                table: "Residents",
                column: "MissionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Residents_Missions_MissionId1",
                table: "Residents",
                column: "MissionId1",
                principalTable: "Missions",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
