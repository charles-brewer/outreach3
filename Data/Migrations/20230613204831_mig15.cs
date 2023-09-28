using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Churches_ChurchesChurchId",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "ChurchesChurchId",
                table: "Missions",
                newName: "ChurchId");

            migrationBuilder.RenameIndex(
                name: "IX_Missions_ChurchesChurchId",
                table: "Missions",
                newName: "IX_Missions_ChurchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Churches_ChurchId",
                table: "Missions",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "ChurchId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Churches_ChurchId",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "ChurchId",
                table: "Missions",
                newName: "ChurchesChurchId");

            migrationBuilder.RenameIndex(
                name: "IX_Missions_ChurchId",
                table: "Missions",
                newName: "IX_Missions_ChurchesChurchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Churches_ChurchesChurchId",
                table: "Missions",
                column: "ChurchesChurchId",
                principalTable: "Churches",
                principalColumn: "ChurchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
