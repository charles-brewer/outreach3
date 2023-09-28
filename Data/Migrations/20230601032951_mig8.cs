using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapMarker_MissionMaps_MissionMapId",
                table: "MapMarker");

            migrationBuilder.AlterColumn<int>(
                name: "MissionMapId",
                table: "MapMarker",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MapMarker_MissionMaps_MissionMapId",
                table: "MapMarker",
                column: "MissionMapId",
                principalTable: "MissionMaps",
                principalColumn: "MissionMapId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapMarker_MissionMaps_MissionMapId",
                table: "MapMarker");

            migrationBuilder.AlterColumn<int>(
                name: "MissionMapId",
                table: "MapMarker",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MapMarker_MissionMaps_MissionMapId",
                table: "MapMarker",
                column: "MissionMapId",
                principalTable: "MissionMaps",
                principalColumn: "MissionMapId");
        }
    }
}
