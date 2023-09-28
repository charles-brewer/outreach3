using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Churches_ChurchesId",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "ChurchesId",
                table: "Members",
                newName: "ChurchId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_ChurchesId",
                table: "Members",
                newName: "IX_Members_ChurchId");

            migrationBuilder.RenameColumn(
                name: "ChurchesId",
                table: "Churches",
                newName: "ChurchId");

            migrationBuilder.AddColumn<int>(
                name: "ChurchesChurchId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Missions_ChurchesChurchId",
                table: "Missions",
                column: "ChurchesChurchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Churches_ChurchId",
                table: "Members",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "ChurchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Churches_ChurchesChurchId",
                table: "Missions",
                column: "ChurchesChurchId",
                principalTable: "Churches",
                principalColumn: "ChurchId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Churches_ChurchId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Churches_ChurchesChurchId",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Missions_ChurchesChurchId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "ChurchesChurchId",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "ChurchId",
                table: "Members",
                newName: "ChurchesId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_ChurchId",
                table: "Members",
                newName: "IX_Members_ChurchesId");

            migrationBuilder.RenameColumn(
                name: "ChurchId",
                table: "Churches",
                newName: "ChurchesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Churches_ChurchesId",
                table: "Members",
                column: "ChurchesId",
                principalTable: "Churches",
                principalColumn: "ChurchesId");
        }
    }
}
