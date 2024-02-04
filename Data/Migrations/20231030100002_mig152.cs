using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig152 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressCount",
                table: "Residents",
                newName: "NumberOfMissions");

            migrationBuilder.AddColumn<string>(
                name: "TitlesOfMissions",
                table: "Residents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitlesOfMissions",
                table: "Residents");

            migrationBuilder.RenameColumn(
                name: "NumberOfMissions",
                table: "Residents",
                newName: "AddressCount");
        }
    }
}
