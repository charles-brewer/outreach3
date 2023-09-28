using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class Mig74 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Visitations");

            migrationBuilder.AddColumn<int>(
                name: "VisitationType",
                table: "Visitations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitationType",
                table: "Visitations");

            migrationBuilder.AddColumn<bool>(
                name: "Contact",
                table: "Visitations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
