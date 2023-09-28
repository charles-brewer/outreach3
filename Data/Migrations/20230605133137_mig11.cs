using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "number",
                table: "MapMarkers",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "color",
                table: "MapMarkers",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "markerLatLng",
                table: "MapMarkers",
                newName: "LatLng");

            migrationBuilder.RenameColumn(
                name: "markerIcon",
                table: "MapMarkers",
                newName: "Icon");

            migrationBuilder.RenameColumn(
                name: "markerArea",
                table: "MapMarkers",
                newName: "Area");

            migrationBuilder.RenameColumn(
                name: "markerAddress",
                table: "MapMarkers",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "MapMarkers",
                newName: "number");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "MapMarkers",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "MapMarkers",
                newName: "markerAddress");

            migrationBuilder.RenameColumn(
                name: "LatLng",
                table: "MapMarkers",
                newName: "markerLatLng");

            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "MapMarkers",
                newName: "markerIcon");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "MapMarkers",
                newName: "markerArea");
        }
    }
}
