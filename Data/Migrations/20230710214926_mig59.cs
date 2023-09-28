using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig59 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Visitations_VisitationId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_VisitationId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "VisitationId",
                table: "Members");

            migrationBuilder.CreateTable(
                name: "MemberVisitation",
                columns: table => new
                {
                    VisitationsVisitationId = table.Column<int>(type: "int", nullable: false),
                    VisitorsMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberVisitation", x => new { x.VisitationsVisitationId, x.VisitorsMemberId });
                    table.ForeignKey(
                        name: "FK_MemberVisitation_Members_VisitorsMemberId",
                        column: x => x.VisitorsMemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberVisitation_Visitations_VisitationsVisitationId",
                        column: x => x.VisitationsVisitationId,
                        principalTable: "Visitations",
                        principalColumn: "VisitationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberVisitation_VisitorsMemberId",
                table: "MemberVisitation",
                column: "VisitorsMemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberVisitation");

            migrationBuilder.AddColumn<int>(
                name: "VisitationId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_VisitationId",
                table: "Members",
                column: "VisitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Visitations_VisitationId",
                table: "Members",
                column: "VisitationId",
                principalTable: "Visitations",
                principalColumn: "VisitationId");
        }
    }
}
