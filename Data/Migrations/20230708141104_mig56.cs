using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig56 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residents_Missions_MissionId",
                table: "Residents");

            migrationBuilder.DropIndex(
                name: "IX_Residents_MissionId",
                table: "Residents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
