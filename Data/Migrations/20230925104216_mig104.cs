using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig104 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionHistory");

            migrationBuilder.AddColumn<int>(
                name: "ChurchId",
                table: "Visitations",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.DropColumn(
                name: "ChurchId",
                table: "Visitations");

            migrationBuilder.CreateTable(
                name: "MissionHistory",
                columns: table => new
                {
                    MissionHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedDate = table.Column<DateTime>(type: "date", nullable: true),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "date", nullable: true),
                    MissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionHistory", x => x.MissionHistoryId);
                    table.ForeignKey(
                        name: "FK_MissionHistory_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionHistory_MissionId",
                table: "MissionHistory",
                column: "MissionId");
        }
    }
}
