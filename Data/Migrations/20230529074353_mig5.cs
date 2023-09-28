using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChurchMember_Church_ChurchId",
                table: "ChurchMember");

            migrationBuilder.DropForeignKey(
                name: "FK_ChurchMember_Member_MemberId",
                table: "ChurchMember");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Church_ChurchId",
                table: "Member");

            migrationBuilder.DropTable(
                name: "Church");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChurchMember",
                table: "ChurchMember");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.RenameTable(
                name: "ChurchMember",
                newName: "ChurchMembers");

            migrationBuilder.RenameColumn(
                name: "ChurchId",
                table: "Members",
                newName: "ChurchesId");

            migrationBuilder.RenameIndex(
                name: "IX_Member_ChurchId",
                table: "Members",
                newName: "IX_Members_ChurchesId");

            migrationBuilder.RenameIndex(
                name: "IX_ChurchMember_ChurchId",
                table: "ChurchMembers",
                newName: "IX_ChurchMembers_ChurchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChurchMembers",
                table: "ChurchMembers",
                columns: new[] { "MemberId", "ChurchId" });

            migrationBuilder.CreateTable(
                name: "Churches",
                columns: table => new
                {
                    ChurchesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChurchFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChurchAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChurchPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PastorsName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Churches", x => x.ChurchesId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ChurchMembers_Churches_ChurchId",
                table: "ChurchMembers",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "ChurchesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChurchMembers_Members_MemberId",
                table: "ChurchMembers",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Churches_ChurchesId",
                table: "Members",
                column: "ChurchesId",
                principalTable: "Churches",
                principalColumn: "ChurchesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChurchMembers_Churches_ChurchId",
                table: "ChurchMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChurchMembers_Members_MemberId",
                table: "ChurchMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Churches_ChurchesId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "Churches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChurchMembers",
                table: "ChurchMembers");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.RenameTable(
                name: "ChurchMembers",
                newName: "ChurchMember");

            migrationBuilder.RenameColumn(
                name: "ChurchesId",
                table: "Member",
                newName: "ChurchId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_ChurchesId",
                table: "Member",
                newName: "IX_Member_ChurchId");

            migrationBuilder.RenameIndex(
                name: "IX_ChurchMembers_ChurchId",
                table: "ChurchMember",
                newName: "IX_ChurchMember_ChurchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChurchMember",
                table: "ChurchMember",
                columns: new[] { "MemberId", "ChurchId" });

            migrationBuilder.CreateTable(
                name: "Church",
                columns: table => new
                {
                    ChurchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChurchAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChurchFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChurchPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PastorsName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Church", x => x.ChurchId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ChurchMember_Church_ChurchId",
                table: "ChurchMember",
                column: "ChurchId",
                principalTable: "Church",
                principalColumn: "ChurchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChurchMember_Member_MemberId",
                table: "ChurchMember",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Church_ChurchId",
                table: "Member",
                column: "ChurchId",
                principalTable: "Church",
                principalColumn: "ChurchId");
        }
    }
}
