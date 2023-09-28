using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapMarker_MissionMaps_MissionMapId",
                table: "MapMarker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapMarker",
                table: "MapMarker");

            migrationBuilder.RenameTable(
                name: "MapMarker",
                newName: "MapMarkers");

            migrationBuilder.RenameIndex(
                name: "IX_MapMarker_MissionMapId",
                table: "MapMarkers",
                newName: "IX_MapMarkers_MissionMapId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapMarkers",
                table: "MapMarkers",
                column: "MapMarkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapMarkers_MissionMaps_MissionMapId",
                table: "MapMarkers",
                column: "MissionMapId",
                principalTable: "MissionMaps",
                principalColumn: "MissionMapId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapMarkers_MissionMaps_MissionMapId",
                table: "MapMarkers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapMarkers",
                table: "MapMarkers");

            migrationBuilder.RenameTable(
                name: "MapMarkers",
                newName: "MapMarker");

            migrationBuilder.RenameIndex(
                name: "IX_MapMarkers_MissionMapId",
                table: "MapMarker",
                newName: "IX_MapMarker_MissionMapId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapMarker",
                table: "MapMarker",
                column: "MapMarkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapMarker_MissionMaps_MissionMapId",
                table: "MapMarker",
                column: "MissionMapId",
                principalTable: "MissionMaps",
                principalColumn: "MissionMapId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
