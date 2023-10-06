using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig108 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FollowUp_ResidentId",
                table: "FollowUp");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUp_ResidentId",
                table: "FollowUp",
                column: "ResidentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FollowUp_ResidentId",
                table: "FollowUp");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUp_ResidentId",
                table: "FollowUp",
                column: "ResidentId");
        }
    }
}
