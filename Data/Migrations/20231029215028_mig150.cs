using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig150 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressCount",
                table: "Residents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressCount",
                table: "Residents");
        }
    }
}
