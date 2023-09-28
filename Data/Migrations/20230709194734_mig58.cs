using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig58 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Visitation_VisitationId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitation_Residents_ResidentId",
                table: "Visitation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visitation",
                table: "Visitation");

            migrationBuilder.RenameTable(
                name: "Visitation",
                newName: "Visitations");

            migrationBuilder.RenameIndex(
                name: "IX_Visitation_ResidentId",
                table: "Visitations",
                newName: "IX_Visitations_ResidentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visitations",
                table: "Visitations",
                column: "VisitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Visitations_VisitationId",
                table: "Members",
                column: "VisitationId",
                principalTable: "Visitations",
                principalColumn: "VisitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitations_Residents_ResidentId",
                table: "Visitations",
                column: "ResidentId",
                principalTable: "Residents",
                principalColumn: "ResidentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Visitations_VisitationId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitations_Residents_ResidentId",
                table: "Visitations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visitations",
                table: "Visitations");

            migrationBuilder.RenameTable(
                name: "Visitations",
                newName: "Visitation");

            migrationBuilder.RenameIndex(
                name: "IX_Visitations_ResidentId",
                table: "Visitation",
                newName: "IX_Visitation_ResidentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visitation",
                table: "Visitation",
                column: "VisitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Visitation_VisitationId",
                table: "Members",
                column: "VisitationId",
                principalTable: "Visitation",
                principalColumn: "VisitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitation_Residents_ResidentId",
                table: "Visitation",
                column: "ResidentId",
                principalTable: "Residents",
                principalColumn: "ResidentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
