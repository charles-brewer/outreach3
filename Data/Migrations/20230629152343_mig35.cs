using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig35 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aquaintance");

            migrationBuilder.DropTable(
                name: "PrayerFollowUp");

            migrationBuilder.DropTable(
                name: "PrayerRequest");

            migrationBuilder.DropColumn(
                name: "IsMember",
                table: "Residents");

            migrationBuilder.AddColumn<int>(
                name: "VisitationId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Visitation",
                columns: table => new
                {
                    VisitationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResidentId = table.Column<int>(type: "int", nullable: true),
                    VisitationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitation", x => x.VisitationId);
                    table.ForeignKey(
                        name: "FK_Visitation_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "ResidentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_VisitationId",
                table: "Members",
                column: "VisitationId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitation_ResidentId",
                table: "Visitation",
                column: "ResidentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Visitation_VisitationId",
                table: "Members",
                column: "VisitationId",
                principalTable: "Visitation",
                principalColumn: "VisitationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Visitation_VisitationId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "Visitation");

            migrationBuilder.DropIndex(
                name: "IX_Members_VisitationId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "VisitationId",
                table: "Members");

            migrationBuilder.AddColumn<bool>(
                name: "IsMember",
                table: "Residents",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrayerRequest",
                columns: table => new
                {
                    PrayerRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcquaintanceId = table.Column<int>(type: "int", nullable: false),
                    MinistryId = table.Column<int>(type: "int", nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResidentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrayerRequest", x => x.PrayerRequestId);
                });

            migrationBuilder.CreateTable(
                name: "Aquaintance",
                columns: table => new
                {
                    AquaintanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestPrayerRequestId = table.Column<int>(type: "int", nullable: false),
                    ResidentId = table.Column<int>(type: "int", nullable: false),
                    AcquaintanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eight = table.Column<bool>(type: "bit", nullable: false),
                    Five = table.Column<bool>(type: "bit", nullable: false),
                    Four = table.Column<bool>(type: "bit", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nine = table.Column<bool>(type: "bit", nullable: false),
                    One = table.Column<bool>(type: "bit", nullable: false),
                    PrayerHeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrayerNeeds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestBusService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seven = table.Column<bool>(type: "bit", nullable: false),
                    ShortDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Six = table.Column<bool>(type: "bit", nullable: false),
                    Ten = table.Column<bool>(type: "bit", nullable: false),
                    Three = table.Column<bool>(type: "bit", nullable: false),
                    Two = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aquaintance", x => x.AquaintanceId);
                    table.ForeignKey(
                        name: "FK_Aquaintance_PrayerRequest_RequestPrayerRequestId",
                        column: x => x.RequestPrayerRequestId,
                        principalTable: "PrayerRequest",
                        principalColumn: "PrayerRequestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aquaintance_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "ResidentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrayerFollowUp",
                columns: table => new
                {
                    PrayerFollowUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FollowUpText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrayerRequestId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrayerFollowUp", x => x.PrayerFollowUpId);
                    table.ForeignKey(
                        name: "FK_PrayerFollowUp_PrayerRequest_PrayerRequestId",
                        column: x => x.PrayerRequestId,
                        principalTable: "PrayerRequest",
                        principalColumn: "PrayerRequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aquaintance_RequestPrayerRequestId",
                table: "Aquaintance",
                column: "RequestPrayerRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Aquaintance_ResidentId",
                table: "Aquaintance",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_PrayerFollowUp_PrayerRequestId",
                table: "PrayerFollowUp",
                column: "PrayerRequestId");
        }
    }
}
