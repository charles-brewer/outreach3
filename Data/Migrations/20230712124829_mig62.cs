using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig62 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberVisitation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
