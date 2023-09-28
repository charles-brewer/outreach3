using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class single4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Churches_ChurchId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "ChurchMembers");

            migrationBuilder.DropIndex(
                name: "IX_Members_ChurchId",
                table: "Members");

            migrationBuilder.AlterColumn<int>(
                name: "ChurchId",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Members");

            migrationBuilder.AlterColumn<int>(
                name: "ChurchId",
                table: "Members",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ChurchMembers",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ChurchId = table.Column<int>(type: "int", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChurchMembers", x => new { x.MemberId, x.ChurchId });
                    table.ForeignKey(
                        name: "FK_ChurchMembers_Churches_ChurchId",
                        column: x => x.ChurchId,
                        principalTable: "Churches",
                        principalColumn: "ChurchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChurchMembers_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_ChurchId",
                table: "Members",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchMembers_ChurchId",
                table: "ChurchMembers",
                column: "ChurchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Churches_ChurchId",
                table: "Members",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "ChurchId");
        }
    }
}
