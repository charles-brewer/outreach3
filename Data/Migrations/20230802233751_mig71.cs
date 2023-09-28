using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig71 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Residents",
                newName: "ForeColor");

            migrationBuilder.AddColumn<string>(
                name: "BackColor",
                table: "Residents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackColor",
                table: "Residents");

            migrationBuilder.RenameColumn(
                name: "ForeColor",
                table: "Residents",
                newName: "Color");
        }
    }
}
