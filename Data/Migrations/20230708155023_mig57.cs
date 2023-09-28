using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig57 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitation_Residents_ResidentId",
                table: "Visitation");

            migrationBuilder.AlterColumn<int>(
                name: "ResidentId",
                table: "Visitation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitation_Residents_ResidentId",
                table: "Visitation",
                column: "ResidentId",
                principalTable: "Residents",
                principalColumn: "ResidentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitation_Residents_ResidentId",
                table: "Visitation");

            migrationBuilder.AlterColumn<int>(
                name: "ResidentId",
                table: "Visitation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitation_Residents_ResidentId",
                table: "Visitation",
                column: "ResidentId",
                principalTable: "Residents",
                principalColumn: "ResidentId");
        }
    }
}
