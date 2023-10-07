using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig109 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowUp_Residents_ResidentId",
                table: "FollowUp");

            migrationBuilder.DropIndex(
                name: "IX_FollowUp_ResidentId",
                table: "FollowUp");

            migrationBuilder.AddColumn<int>(
                name: "FollowUpId",
                table: "Residents",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowUpId",
                table: "Residents");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUp_ResidentId",
                table: "FollowUp",
                column: "ResidentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUp_Residents_ResidentId",
                table: "FollowUp",
                column: "ResidentId",
                principalTable: "Residents",
                principalColumn: "ResidentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
